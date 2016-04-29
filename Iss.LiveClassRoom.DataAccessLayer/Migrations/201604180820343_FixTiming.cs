namespace Iss.LiveClassRoom.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixTiming : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Comments", "TimeCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.Comments", "TimeDeletedUtc", c => c.DateTime());
            AddColumn("dbo.Feeds", "TimeCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Feeds", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.Feeds", "TimeDeletedUtc", c => c.DateTime());
            AddColumn("dbo.Courses", "TimeCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Courses", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.Courses", "TimeDeletedUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TimeCreatedUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TimeDeletedUtc", c => c.DateTime());
            AddColumn("dbo.Quizs", "TimeCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quizs", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.Quizs", "TimeDeletedUtc", c => c.DateTime());
            AddColumn("dbo.QuizOptions", "TimeCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizOptions", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.QuizOptions", "TimeDeletedUtc", c => c.DateTime());
            AddColumn("dbo.StudentAnswers", "TimeCreatedUtc", c => c.DateTime(nullable: false));
            AddColumn("dbo.StudentAnswers", "TimeModifiedUtc", c => c.DateTime());
            AddColumn("dbo.StudentAnswers", "TimeDeletedUtc", c => c.DateTime());
            DropColumn("dbo.Comments", "Time");
            DropColumn("dbo.Comments", "TimeCreated");
            DropColumn("dbo.Comments", "TimeModified");
            DropColumn("dbo.Comments", "TimeDeleted");
            DropColumn("dbo.Feeds", "TimeCreated");
            DropColumn("dbo.Feeds", "TimeModified");
            DropColumn("dbo.Feeds", "TimeDeleted");
            DropColumn("dbo.Videos", "Time");
            DropColumn("dbo.Courses", "TimeCreated");
            DropColumn("dbo.Courses", "TimeModified");
            DropColumn("dbo.Courses", "TimeDeleted");
            DropColumn("dbo.AspNetUsers", "TimeCreated");
            DropColumn("dbo.AspNetUsers", "TimeModified");
            DropColumn("dbo.AspNetUsers", "TimeDeleted");
            DropColumn("dbo.Quizs", "TimeCreated");
            DropColumn("dbo.Quizs", "TimeModified");
            DropColumn("dbo.Quizs", "TimeDeleted");
            DropColumn("dbo.QuizOptions", "TimeCreated");
            DropColumn("dbo.QuizOptions", "TimeModified");
            DropColumn("dbo.QuizOptions", "TimeDeleted");
            DropColumn("dbo.StudentAnswers", "TimeCreated");
            DropColumn("dbo.StudentAnswers", "TimeModified");
            DropColumn("dbo.StudentAnswers", "TimeDeleted");
        }
        
        public override void Down()
        {
            AddColumn("dbo.StudentAnswers", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.StudentAnswers", "TimeModified", c => c.DateTime());
            AddColumn("dbo.StudentAnswers", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.QuizOptions", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.QuizOptions", "TimeModified", c => c.DateTime());
            AddColumn("dbo.QuizOptions", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quizs", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Quizs", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Quizs", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TimeModified", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "TimeCreated", c => c.DateTime());
            AddColumn("dbo.Courses", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Courses", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Courses", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Videos", "Time", c => c.DateTime(nullable: false));
            AddColumn("dbo.Feeds", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Feeds", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Feeds", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "TimeDeleted", c => c.DateTime());
            AddColumn("dbo.Comments", "TimeModified", c => c.DateTime());
            AddColumn("dbo.Comments", "TimeCreated", c => c.DateTime(nullable: false));
            AddColumn("dbo.Comments", "Time", c => c.DateTime(nullable: false));
            DropColumn("dbo.StudentAnswers", "TimeDeletedUtc");
            DropColumn("dbo.StudentAnswers", "TimeModifiedUtc");
            DropColumn("dbo.StudentAnswers", "TimeCreatedUtc");
            DropColumn("dbo.QuizOptions", "TimeDeletedUtc");
            DropColumn("dbo.QuizOptions", "TimeModifiedUtc");
            DropColumn("dbo.QuizOptions", "TimeCreatedUtc");
            DropColumn("dbo.Quizs", "TimeDeletedUtc");
            DropColumn("dbo.Quizs", "TimeModifiedUtc");
            DropColumn("dbo.Quizs", "TimeCreatedUtc");
            DropColumn("dbo.AspNetUsers", "TimeDeletedUtc");
            DropColumn("dbo.AspNetUsers", "TimeModifiedUtc");
            DropColumn("dbo.AspNetUsers", "TimeCreatedUtc");
            DropColumn("dbo.Courses", "TimeDeletedUtc");
            DropColumn("dbo.Courses", "TimeModifiedUtc");
            DropColumn("dbo.Courses", "TimeCreatedUtc");
            DropColumn("dbo.Feeds", "TimeDeletedUtc");
            DropColumn("dbo.Feeds", "TimeModifiedUtc");
            DropColumn("dbo.Feeds", "TimeCreatedUtc");
            DropColumn("dbo.Comments", "TimeDeletedUtc");
            DropColumn("dbo.Comments", "TimeModifiedUtc");
            DropColumn("dbo.Comments", "TimeCreatedUtc");
        }
    }
}
