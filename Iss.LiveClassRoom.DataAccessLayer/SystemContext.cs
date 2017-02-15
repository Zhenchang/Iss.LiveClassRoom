using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.DataAccessLayer
{
    public class SoftDeleteHelper {
        public static string GetSoftDeleteColumnName(EdmType type) {
            // TODO Find the soft delete annotation and get the property name
            //      Name of annotation will be something like: 
            //      http://schemas.microsoft.com/ado/2013/11/edm/customannotation:SoftDeleteColumnName

            MetadataProperty annotation = type.MetadataProperties
                .Where(p => p.Name.EndsWith("customannotation:SoftDeleteColumnName"))
                .SingleOrDefault();

            return annotation == null ? null : (string)annotation.Value;
        }
        public static string GetSoftDeleteDateColumnName(EdmType type) {
            // TODO Find the soft delete annotation and get the property name
            //      Name of annotation will be something like: 
            //      http://schemas.microsoft.com/ado/2013/11/edm/customannotation:SoftDeleteDateColumnName

            MetadataProperty annotation = type.MetadataProperties
                .Where(p => p.Name.EndsWith("customannotation:SoftDeleteDateColumnName"))
                .SingleOrDefault();

            return annotation == null ? null : (string)annotation.Value;
        }
    }

    public class SoftDeleteInterceptor : IDbCommandTreeInterceptor {
        public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext) {
            if (interceptionContext.OriginalResult.DataSpace == DataSpace.SSpace) {
                var queryCommand = interceptionContext.Result as DbQueryCommandTree;
                if (queryCommand != null) {
                    var newQuery = queryCommand.Query.Accept(new SoftDeleteQueryVisitor());
                    interceptionContext.Result = new DbQueryCommandTree(
                        queryCommand.MetadataWorkspace,
                        queryCommand.DataSpace,
                        newQuery);
                }

                var deleteCommand = interceptionContext.OriginalResult as DbDeleteCommandTree;
                if (deleteCommand != null) {
                    var column = SoftDeleteHelper.GetSoftDeleteColumnName(deleteCommand.Target.VariableType.EdmType);
                    if (column != null) {
                        // Just because the entity has the soft delete annotation doesn't mean that 
                        // this particular table has the column. This occurs in situation like TPT
                        // inheritance mapping and entity splitting where one type maps to multiple 
                        // tables.
                        // If the table doesn't have the column we just want to leave the row unchanged
                        // since it will be joined to the table that does have the column during query.
                        // We can't no-op, so we just generate an UPDATE command that doesn't set anything.
                        var setClauses = new List<DbModificationClause>();
                        var table = (EntityType)deleteCommand.Target.VariableType.EdmType;
                        if (table.Properties.Any(p => p.Name == column)) {
                            setClauses.Add(DbExpressionBuilder.SetClause(
                                    DbExpressionBuilder.Property(
                                        DbExpressionBuilder.Variable(deleteCommand.Target.VariableType, deleteCommand.Target.VariableName),
                                        column),
                                    DbExpression.FromBoolean(true)));

                            var dateColumn = SoftDeleteHelper.GetSoftDeleteDateColumnName(deleteCommand.Target.VariableType.EdmType);
                            setClauses.Add(DbExpressionBuilder.SetClause(
                                    DbExpressionBuilder.Property(
                                        DbExpressionBuilder.Variable(deleteCommand.Target.VariableType, deleteCommand.Target.VariableName),
                                        dateColumn),
                                    DbExpression.FromDateTime(DateTime.UtcNow)));
                        }

                        var update = new DbUpdateCommandTree(
                            deleteCommand.MetadataWorkspace,
                            deleteCommand.DataSpace,
                            deleteCommand.Target,
                            deleteCommand.Predicate,
                            setClauses.AsReadOnly(),
                            null);

                        interceptionContext.Result = update;
                    }
                }
            }
        }
    }

    public class SoftDeleteQueryVisitor : DefaultExpressionVisitor {
        public override DbExpression Visit(DbScanExpression expression) {
            var column = SoftDeleteHelper.GetSoftDeleteColumnName(expression.Target.ElementType);
            if (column != null) {
                // Just because the entity has the soft delete annotation doesn't mean that 
                // this particular table has the column. This occurs in situation like TPT
                // inheritance mapping and entity splitting where one type maps to multiple 
                // tables.
                // We only apply the filter if the column is actually present in this table.
                // If not, then the query is going to be joining to the table that does have
                // the column anyway, so the filter will still be applied.
                var table = (EntityType)expression.Target.ElementType;
                if (table.Properties.Any(p => p.Name == column)) {
                    var binding = DbExpressionBuilder.Bind(expression);
                    return DbExpressionBuilder.Filter(
                        binding,
                        DbExpressionBuilder.NotEqual(
                            DbExpressionBuilder.Property(
                                DbExpressionBuilder.Variable(binding.VariableType, binding.VariableName),
                                column),
                            DbExpression.FromBoolean(true)));
                }
            }

            return base.Visit(expression);
        }
    }

    public class EntityFrameworkConfiguration : DbConfiguration {
        public EntityFrameworkConfiguration() {
            AddInterceptor(new SoftDeleteInterceptor());
        }
    }

    public class SystemContext : DbContext {

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public DbSet<EnrollmentRequest> EnrollmentRequests { get; set; }

        public string Name { get; set; }
        public SystemContext()
            : base("LiveClassRoomDb") {
            Name = Guid.NewGuid().ToString();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {

            var convSoftDelete = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
             "SoftDeleteColumnName",
             (type, attributes) => attributes.Single().ColumnName);

            var convDeleteDate = new AttributeToTableAnnotationConvention<SoftDeleteAttribute, string>(
             "SoftDeleteDateColumnName",
             (type, attributes) => attributes.Single().DateColumnName);

            modelBuilder.Conventions.Add(convSoftDelete);
            modelBuilder.Conventions.Add(convDeleteDate);


            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<Topic>().ToTable("Topics");

            modelBuilder.Entity<Instructor>().ToTable("Instructors");
            modelBuilder.Entity<Student>().ToTable("Students");

            base.OnModelCreating(modelBuilder);
        }


        public override Task<int> SaveChangesAsync() {
            return base.SaveChangesAsync();
        }

        public Task<int> SaveChangesAsync(string userId) {
            foreach (var e in ChangeTracker.Entries()) {
                if (e.Entity is IEntity) {
                    var entity = ((IEntity)e.Entity);
                    switch (e.State) {
                        case EntityState.Added:
                            entity.TimeCreatedUtc = DateTime.UtcNow;
                            entity.CreatedByUserId = userId;
                            break;
                        case EntityState.Deleted:
                            entity.TimeDeletedUtc = DateTime.UtcNow;
                            entity.DeletedByUserId = userId;
                            entity.IsDeleted = true;
                            break;
                        case EntityState.Modified:
                            entity.TimeModifiedUtc = DateTime.UtcNow;
                            entity.ModifiedByUserId = userId;
                            break;
                    }
                }
            }
            return SaveChangesAsync();
        }

        public int SaveChanges(string userId)
        {
            foreach (var e in ChangeTracker.Entries())
            {
                if (e.Entity is IEntity)
                {
                    var entity = ((IEntity)e.Entity);
                    switch (e.State)
                    {
                        case EntityState.Added:
                            entity.TimeCreatedUtc = DateTime.UtcNow;
                            entity.CreatedByUserId = userId;
                            break;
                        case EntityState.Deleted:
                            entity.TimeDeletedUtc = DateTime.UtcNow;
                            entity.DeletedByUserId = userId;
                            entity.IsDeleted = true;
                            break;
                        case EntityState.Modified:
                            entity.TimeModifiedUtc = DateTime.UtcNow;
                            entity.ModifiedByUserId = userId;
                            break;
                    }
                }
            }
            return base.SaveChanges();
        }
    }
}
