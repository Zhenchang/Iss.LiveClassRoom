using Iss.LiveClassRoom.Core.Models;
using Iss.LiveClassRoom.Core.Repositories;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Iss.LiveClassRoom.DataAccessLayer
{
    public class SystemContext : DbContext {

        public DbSet<User> Users { get; set; }

        public DbSet<Quiz> Quizes { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Reply> Replies { get; set; }
        public DbSet<Video> Videos { get; set; }
        public DbSet<Topic> Topics { get; set; }

        public SystemContext()
            : base("LiveClassRoomDb") {
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder) {
            modelBuilder.Entity<Video>().ToTable("Videos");
            modelBuilder.Entity<Topic>().ToTable("Topics");

            modelBuilder.Entity<Instructor>().ToTable("Instructors");
            modelBuilder.Entity<Student>().ToTable("Students");

            base.OnModelCreating(modelBuilder);
        }
    }
}
