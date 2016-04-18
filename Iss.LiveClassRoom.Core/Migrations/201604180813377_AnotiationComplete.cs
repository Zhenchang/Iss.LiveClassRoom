namespace Iss.LiveClassRoom.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AnotiationComplete : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.AspNetUsers", newName: "Instructors");
            DropForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Topics", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions");
            DropIndex("dbo.Comments", new[] { "Feed_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            DropIndex("dbo.Instructors", "UserNameIndex");
            DropIndex("dbo.Quizs", new[] { "Course_Id" });
            DropIndex("dbo.QuizOptions", new[] { "Quiz_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "QuizOption_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "Student_Id" });
            DropIndex("dbo.Topics", new[] { "Course_Id" });
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                        Name = c.String(maxLength: 50),
                        IsDeleted = c.Boolean(),
                        TimeCreated = c.DateTime(),
                        TimeModified = c.DateTime(),
                        TimeDeleted = c.DateTime(),
                        CreatedByUserId = c.String(),
                        ModifiedByUserId = c.String(),
                        DeletedByUserId = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Id)
                .Index(t => t.Id);
            
            AddColumn("dbo.Comments", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Comments", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Comments", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Comments", "CreatedByUserId", c => c.String(nullable: false));
            AddColumn("dbo.Comments", "ModifiedByUserId", c => c.String());
            AddColumn("dbo.Comments", "DeletedByUserId", c => c.String());
            AddColumn("dbo.Feeds", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Feeds", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Feeds", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Feeds", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Feeds", "CreatedByUserId", c => c.String(nullable: false));
            AddColumn("dbo.Feeds", "ModifiedByUserId", c => c.String());
            AddColumn("dbo.Feeds", "DeletedByUserId", c => c.String());
            AddColumn("dbo.Topics", "Content", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Courses", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Courses", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Courses", "CreatedByUserId", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "ModifiedByUserId", c => c.String());
            AddColumn("dbo.Courses", "DeletedByUserId", c => c.String());
            AddColumn("dbo.Quizs", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.Quizs", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quizs", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Quizs", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Quizs", "CreatedByUserId", c => c.String(nullable: false));
            AddColumn("dbo.Quizs", "ModifiedByUserId", c => c.String());
            AddColumn("dbo.Quizs", "DeletedByUserId", c => c.String());
            AddColumn("dbo.QuizOptions", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.QuizOptions", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizOptions", "TimeModified", c => c.DateTime());
            AddColumn("dbo.QuizOptions", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.QuizOptions", "CreatedByUserId", c => c.String(nullable: false));
            AddColumn("dbo.QuizOptions", "ModifiedByUserId", c => c.String());
            AddColumn("dbo.QuizOptions", "DeletedByUserId", c => c.String());
            AddColumn("dbo.StudentAnswers", "IsDeleted", c => c.Boolean(nullable: false));
            AddColumn("dbo.StudentAnswers", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.StudentAnswers", "TimeModified", c => c.DateTime());
            AddColumn("dbo.StudentAnswers", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.StudentAnswers", "CreatedByUserId", c => c.String(nullable: false));
            AddColumn("dbo.StudentAnswers", "ModifiedByUserId", c => c.String());
            AddColumn("dbo.StudentAnswers", "DeletedByUserId", c => c.String());
            AlterColumn("dbo.Comments", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.Comments", "Feed_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Comments", "User_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Topics", "Course_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Videos", "Course_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Videos", "Title", c => c.String(nullable: false));
            AlterColumn("dbo.Videos", "FileName", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            AlterColumn("dbo.Courses", "Instructor_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Quizs", "Question", c => c.String(nullable: false));
            AlterColumn("dbo.Quizs", "Course_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.QuizOptions", "Text", c => c.String(nullable: false));
            AlterColumn("dbo.QuizOptions", "Quiz_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.StudentAnswers", "QuizOption_Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.StudentAnswers", "Student_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "Feed_Id");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Courses", "Instructor_Id");
            CreateIndex("dbo.Quizs", "Course_Id");
            CreateIndex("dbo.QuizOptions", "Quiz_Id");
            CreateIndex("dbo.StudentAnswers", "QuizOption_Id");
            CreateIndex("dbo.StudentAnswers", "Student_Id");
            CreateIndex("dbo.Topics", "Course_Id");
            CreateIndex("dbo.Videos", "Course_Id");
            CreateIndex("dbo.Instructors", "Id");
            AddForeignKey("dbo.Instructors", "Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Topics", "Course_Id", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Videos", "Course_Id", "dbo.Courses", "Id", cascadeDelete: true);
            AddForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs", "Id", cascadeDelete: true);
            AddForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions", "Id", cascadeDelete: true);
            DropColumn("dbo.Instructors", "Email");
            DropColumn("dbo.Instructors", "EmailConfirmed");
            DropColumn("dbo.Instructors", "PasswordHash");
            DropColumn("dbo.Instructors", "SecurityStamp");
            DropColumn("dbo.Instructors", "PhoneNumber");
            DropColumn("dbo.Instructors", "PhoneNumberConfirmed");
            DropColumn("dbo.Instructors", "TwoFactorEnabled");
            DropColumn("dbo.Instructors", "LockoutEndDateUtc");
            DropColumn("dbo.Instructors", "LockoutEnabled");
            DropColumn("dbo.Instructors", "AccessFailedCount");
            DropColumn("dbo.Instructors", "UserName");
            DropColumn("dbo.Instructors", "Name");
            DropColumn("dbo.Instructors", "Discriminator");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Instructors", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.Instructors", "Name", c => c.String(maxLength: 50));
            AddColumn("dbo.Instructors", "UserName", c => c.String(nullable: false, maxLength: 256));
            AddColumn("dbo.Instructors", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.Instructors", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Instructors", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.Instructors", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.Instructors", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Instructors", "PhoneNumber", c => c.String());
            AddColumn("dbo.Instructors", "SecurityStamp", c => c.String());
            AddColumn("dbo.Instructors", "PasswordHash", c => c.String());
            AddColumn("dbo.Instructors", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.Instructors", "Email", c => c.String(maxLength: 256));
            DropForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions");
            DropForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Topics", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds");
            DropForeignKey("dbo.Students", "Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Instructors", "Id", "dbo.AspNetUsers");
            DropIndex("dbo.Students", new[] { "Id" });
            DropIndex("dbo.Instructors", new[] { "Id" });
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            DropIndex("dbo.Topics", new[] { "Course_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "Student_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "QuizOption_Id" });
            DropIndex("dbo.QuizOptions", new[] { "Quiz_Id" });
            DropIndex("dbo.Quizs", new[] { "Course_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            DropIndex("dbo.Comments", new[] { "User_Id" });
            DropIndex("dbo.Comments", new[] { "Feed_Id" });
            AlterColumn("dbo.StudentAnswers", "Student_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentAnswers", "QuizOption_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.QuizOptions", "Quiz_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.QuizOptions", "Text", c => c.String());
            AlterColumn("dbo.Quizs", "Course_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Quizs", "Question", c => c.String());
            AlterColumn("dbo.Courses", "Instructor_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Courses", "Name", c => c.String());
            AlterColumn("dbo.Videos", "FileName", c => c.String());
            AlterColumn("dbo.Videos", "Title", c => c.String());
            AlterColumn("dbo.Videos", "Course_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Topics", "Course_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Comments", "User_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "Feed_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Comments", "Text", c => c.String());
            DropColumn("dbo.StudentAnswers", "DeletedByUserId");
            DropColumn("dbo.StudentAnswers", "ModifiedByUserId");
            DropColumn("dbo.StudentAnswers", "CreatedByUserId");
            DropColumn("dbo.StudentAnswers", "TimeDeleted");
            DropColumn("dbo.StudentAnswers", "TimeModified");
            DropColumn("dbo.StudentAnswers", "TimeCreated");
            DropColumn("dbo.StudentAnswers", "IsDeleted");
            DropColumn("dbo.QuizOptions", "DeletedByUserId");
            DropColumn("dbo.QuizOptions", "ModifiedByUserId");
            DropColumn("dbo.QuizOptions", "CreatedByUserId");
            DropColumn("dbo.QuizOptions", "TimeDeleted");
            DropColumn("dbo.QuizOptions", "TimeModified");
            DropColumn("dbo.QuizOptions", "TimeCreated");
            DropColumn("dbo.QuizOptions", "IsDeleted");
            DropColumn("dbo.Quizs", "DeletedByUserId");
            DropColumn("dbo.Quizs", "ModifiedByUserId");
            DropColumn("dbo.Quizs", "CreatedByUserId");
            DropColumn("dbo.Quizs", "TimeDeleted");
            DropColumn("dbo.Quizs", "TimeModified");
            DropColumn("dbo.Quizs", "TimeCreated");
            DropColumn("dbo.Quizs", "IsDeleted");
            DropColumn("dbo.Courses", "DeletedByUserId");
            DropColumn("dbo.Courses", "ModifiedByUserId");
            DropColumn("dbo.Courses", "CreatedByUserId");
            DropColumn("dbo.Courses", "TimeDeleted");
            DropColumn("dbo.Courses", "TimeModified");
            DropColumn("dbo.Courses", "TimeCreated");
            DropColumn("dbo.Courses", "IsDeleted");
            DropColumn("dbo.Topics", "Content");
            DropColumn("dbo.Feeds", "DeletedByUserId");
            DropColumn("dbo.Feeds", "ModifiedByUserId");
            DropColumn("dbo.Feeds", "CreatedByUserId");
            DropColumn("dbo.Feeds", "TimeDeleted");
            DropColumn("dbo.Feeds", "TimeModified");
            DropColumn("dbo.Feeds", "TimeCreated");
            DropColumn("dbo.Feeds", "IsDeleted");
            DropColumn("dbo.Comments", "DeletedByUserId");
            DropColumn("dbo.Comments", "ModifiedByUserId");
            DropColumn("dbo.Comments", "CreatedByUserId");
            DropColumn("dbo.Comments", "TimeDeleted");
            DropColumn("dbo.Comments", "TimeModified");
            DropColumn("dbo.Comments", "TimeCreated");
            DropColumn("dbo.Comments", "IsDeleted");
            DropTable("dbo.Students");
            DropTable("dbo.AspNetUsers");
            CreateIndex("dbo.Videos", "Course_Id");
            CreateIndex("dbo.Topics", "Course_Id");
            CreateIndex("dbo.StudentAnswers", "Student_Id");
            CreateIndex("dbo.StudentAnswers", "QuizOption_Id");
            CreateIndex("dbo.QuizOptions", "Quiz_Id");
            CreateIndex("dbo.Quizs", "Course_Id");
            CreateIndex("dbo.Instructors", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Courses", "Instructor_Id");
            CreateIndex("dbo.Comments", "User_Id");
            CreateIndex("dbo.Comments", "Feed_Id");
            AddForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions", "Id");
            AddForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs", "Id");
            AddForeignKey("dbo.Videos", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Topics", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Comments", "User_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds", "Id");
            RenameTable(name: "dbo.Instructors", newName: "AspNetUsers");
        }
    }
}
