namespace Iss.LiveClassRoom.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixFeed : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        Time = c.DateTime(nullable: false),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Feed_Id = c.String(maxLength: 128),
                        Comment_Id = c.String(maxLength: 128),
                        User_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.Feed_Id)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .ForeignKey("dbo.Users", t => t.User_Id)
                .Index(t => t.Feed_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Feeds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Instructor_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Instructor_Id)
                .Index(t => t.Instructor_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                        Email = c.String(),
                        Password = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Question = c.String(),
                        Course_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.QuizOptions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Text = c.String(),
                        Quiz_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.Quiz_Id)
                .Index(t => t.Quiz_Id);
            
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Time = c.DateTime(nullable: false),
                        QuizOption_Id = c.String(maxLength: 128),
                        Student_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizOptions", t => t.QuizOption_Id)
                .ForeignKey("dbo.Users", t => t.Student_Id)
                .Index(t => t.QuizOption_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Course_Id = c.String(maxLength: 128),
                        Title = c.String(),
                        Time = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id)
                .Index(t => t.Id)
                .Index(t => t.Course_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Videos", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Topics", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Topics", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.StudentAnswers", "Student_Id", "dbo.Users");
            DropForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions");
            DropForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Instructor_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds");
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            DropIndex("dbo.Videos", new[] { "Id" });
            DropIndex("dbo.Topics", new[] { "Course_Id" });
            DropIndex("dbo.Topics", new[] { "Id" });
            DropIndex("dbo.StudentAnswers", new[] { "Student_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "QuizOption_Id" });
            DropIndex("dbo.QuizOptions", new[] { "Quiz_Id" });
            DropIndex("dbo.Quizs", new[] { "Course_Id" });
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropIndex("dbo.Comments", new[] { "Feed_Id" });
            DropTable("dbo.Videos");
            DropTable("dbo.Topics");
            DropTable("dbo.StudentAnswers");
            DropTable("dbo.QuizOptions");
            DropTable("dbo.Quizs");
            DropTable("dbo.Users");
            DropTable("dbo.Courses");
            DropTable("dbo.Feeds");
            DropTable("dbo.Comments");
        }
    }
}
