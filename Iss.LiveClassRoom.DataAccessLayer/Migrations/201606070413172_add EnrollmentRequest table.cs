namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class addEnrollmentRequesttable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EnrollmentRequests",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        StudentId = c.String(),
                        CourseId = c.String(),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                    },
                annotations: new Dictionary<string, object>
                {
                    { "SoftDeleteColumnName", "IsDeleted" },
                    { "SoftDeleteDateColumnName", "TimeDeletedUtc" },
                })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.EnrollmentRequests",
                removedAnnotations: new Dictionary<string, object>
                {
                    { "SoftDeleteColumnName", "IsDeleted" },
                    { "SoftDeleteDateColumnName", "TimeDeletedUtc" },
                });
        }
    }
}
