namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Text = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Feed_Id = c.String(nullable: false, maxLength: 32),
                        Comment_Id = c.String(maxLength: 32),
                        User_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.Feed_Id, cascadeDelete: true)
                .ForeignKey("dbo.Comments", t => t.Comment_Id)
                .ForeignKey("dbo.Users", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.Feed_Id)
                .Index(t => t.Comment_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Feeds",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Courses",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                        Instructor_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Instructors", t => t.Instructor_Id)
                .Index(t => t.Instructor_Id);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Name = c.String(nullable: false, maxLength: 50),
                        Email = c.String(nullable: false),
                        PasswordHash = c.String(nullable: false),
                        PhoneNumber = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Quizs",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Question = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.QuizOptions",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Text = c.String(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                        Quiz_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Quizs", t => t.Quiz_Id, cascadeDelete: true)
                .Index(t => t.Quiz_Id);
            
            CreateTable(
                "dbo.StudentAnswers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Time = c.DateTime(nullable: false),
                        IsDeleted = c.Boolean(nullable: false),
                        TimeCreatedUtc = c.DateTime(nullable: false),
                        TimeModifiedUtc = c.DateTime(),
                        TimeDeletedUtc = c.DateTime(),
                        CreatedByUserId = c.String(nullable: false),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                        QuizOption_Id = c.String(nullable: false, maxLength: 32),
                        Student_Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.QuizOptions", t => t.QuizOption_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id)
                .Index(t => t.QuizOption_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.Topics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                        Content = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                        Title = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Feeds", t => t.Id)
                .ForeignKey("dbo.Courses", t => t.Course_Id, cascadeDelete: true)
                .Index(t => t.Id)
                .Index(t => t.Course_Id);
            
            CreateTable(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Users", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Students", "Id", "dbo.Users");
            DropForeignKey("dbo.Instructors", "Id", "dbo.Users");
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Videos", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Topics", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Topics", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.Users");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds");
            DropForeignKey("dbo.StudentAnswers", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions");
            DropForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Courses", "Instructor_Id", "dbo.Instructors");
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Instructors", new[] { "Id" });
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
            DropTable("dbo.Students");
            DropTable("dbo.Instructors");
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
