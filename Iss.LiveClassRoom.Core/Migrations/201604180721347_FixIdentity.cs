namespace Iss.LiveClassRoom.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FixIdentity : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Users", newName: "AspNetUsers");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds");
            DropForeignKey("dbo.Topics", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Videos", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Topics", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions");
            DropIndex("dbo.Comments", new[] { "Feed_Id" });
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropIndex("dbo.Quizs", new[] { "Course_Id" });
            DropIndex("dbo.QuizOptions", new[] { "Quiz_Id" });
            DropIndex("dbo.StudentAnswers", new[] { "QuizOption_Id" });
            DropIndex("dbo.Topics", new[] { "Id" });
            DropIndex("dbo.Topics", new[] { "Course_Id" });
            DropIndex("dbo.Videos", new[] { "Id" });
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            DropPrimaryKey("dbo.Comments");
            DropPrimaryKey("dbo.Feeds");
            DropPrimaryKey("dbo.Topics");
            DropPrimaryKey("dbo.Videos");
            DropPrimaryKey("dbo.Courses");
            DropPrimaryKey("dbo.Quizs");
            DropPrimaryKey("dbo.QuizOptions");
            DropPrimaryKey("dbo.StudentAnswers");
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            AddColumn("dbo.AspNetUsers", "EmailConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "PasswordHash", c => c.String());
            AddColumn("dbo.AspNetUsers", "SecurityStamp", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumber", c => c.String());
            AddColumn("dbo.AspNetUsers", "PhoneNumberConfirmed", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "TwoFactorEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "LockoutEndDateUtc", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "LockoutEnabled", c => c.Boolean(nullable: false));
            AddColumn("dbo.AspNetUsers", "AccessFailedCount", c => c.Int(nullable: false));
            AddColumn("dbo.AspNetUsers", "UserName", c => c.String(nullable: false, maxLength: 256));
            AlterColumn("dbo.Comments", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Comments", "Feed_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Comments", "Comment_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Feeds", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Topics", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Topics", "Course_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Videos", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Videos", "Course_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Courses", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String(maxLength: 50));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 256));
            AlterColumn("dbo.Quizs", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.Quizs", "Course_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.QuizOptions", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.QuizOptions", "Quiz_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.StudentAnswers", "Id", c => c.String(nullable: false, maxLength: 32));
            AlterColumn("dbo.StudentAnswers", "QuizOption_Id", c => c.String(maxLength: 32));
            AddPrimaryKey("dbo.Comments", "Id");
            AddPrimaryKey("dbo.Feeds", "Id");
            AddPrimaryKey("dbo.Topics", "Id");
            AddPrimaryKey("dbo.Videos", "Id");
            AddPrimaryKey("dbo.Courses", "Id");
            AddPrimaryKey("dbo.Quizs", "Id");
            AddPrimaryKey("dbo.QuizOptions", "Id");
            AddPrimaryKey("dbo.StudentAnswers", "Id");
            CreateIndex("dbo.Comments", "Feed_Id");
            CreateIndex("dbo.Comments", "Comment_Id");
            CreateIndex("dbo.AspNetUsers", "UserName", unique: true, name: "UserNameIndex");
            CreateIndex("dbo.Quizs", "Course_Id");
            CreateIndex("dbo.QuizOptions", "Quiz_Id");
            CreateIndex("dbo.StudentAnswers", "QuizOption_Id");
            CreateIndex("dbo.Topics", "Id");
            CreateIndex("dbo.Topics", "Course_Id");
            CreateIndex("dbo.Videos", "Id");
            CreateIndex("dbo.Videos", "Course_Id");
            AddForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments", "Id");
            AddForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds", "Id");
            AddForeignKey("dbo.Topics", "Id", "dbo.Feeds", "Id");
            AddForeignKey("dbo.Videos", "Id", "dbo.Feeds", "Id");
            AddForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Topics", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Videos", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs", "Id");
            AddForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions", "Id");
            DropColumn("dbo.AspNetUsers", "Password");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "Password", c => c.String());
            DropForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions");
            DropForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs");
            DropForeignKey("dbo.Videos", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Topics", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses");
            DropForeignKey("dbo.Videos", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Topics", "Id", "dbo.Feeds");
            DropForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds");
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropIndex("dbo.Videos", new[] { "Course_Id" });
            DropIndex("dbo.Videos", new[] { "Id" });
            DropIndex("dbo.Topics", new[] { "Course_Id" });
            DropIndex("dbo.Topics", new[] { "Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.StudentAnswers", new[] { "QuizOption_Id" });
            DropIndex("dbo.QuizOptions", new[] { "Quiz_Id" });
            DropIndex("dbo.Quizs", new[] { "Course_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropIndex("dbo.Comments", new[] { "Feed_Id" });
            DropPrimaryKey("dbo.StudentAnswers");
            DropPrimaryKey("dbo.QuizOptions");
            DropPrimaryKey("dbo.Quizs");
            DropPrimaryKey("dbo.Courses");
            DropPrimaryKey("dbo.Videos");
            DropPrimaryKey("dbo.Topics");
            DropPrimaryKey("dbo.Feeds");
            DropPrimaryKey("dbo.Comments");
            AlterColumn("dbo.StudentAnswers", "QuizOption_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.StudentAnswers", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.QuizOptions", "Quiz_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.QuizOptions", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Quizs", "Course_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Quizs", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "Email", c => c.String());
            AlterColumn("dbo.AspNetUsers", "Name", c => c.String());
            AlterColumn("dbo.Courses", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Videos", "Course_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Videos", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Topics", "Course_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Topics", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Feeds", "Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.Comments", "Comment_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "Feed_Id", c => c.String(maxLength: 128));
            AlterColumn("dbo.Comments", "Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUsers", "UserName");
            DropColumn("dbo.AspNetUsers", "AccessFailedCount");
            DropColumn("dbo.AspNetUsers", "LockoutEnabled");
            DropColumn("dbo.AspNetUsers", "LockoutEndDateUtc");
            DropColumn("dbo.AspNetUsers", "TwoFactorEnabled");
            DropColumn("dbo.AspNetUsers", "PhoneNumberConfirmed");
            DropColumn("dbo.AspNetUsers", "PhoneNumber");
            DropColumn("dbo.AspNetUsers", "SecurityStamp");
            DropColumn("dbo.AspNetUsers", "PasswordHash");
            DropColumn("dbo.AspNetUsers", "EmailConfirmed");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            AddPrimaryKey("dbo.StudentAnswers", "Id");
            AddPrimaryKey("dbo.QuizOptions", "Id");
            AddPrimaryKey("dbo.Quizs", "Id");
            AddPrimaryKey("dbo.Courses", "Id");
            AddPrimaryKey("dbo.Videos", "Id");
            AddPrimaryKey("dbo.Topics", "Id");
            AddPrimaryKey("dbo.Feeds", "Id");
            AddPrimaryKey("dbo.Comments", "Id");
            CreateIndex("dbo.Videos", "Course_Id");
            CreateIndex("dbo.Videos", "Id");
            CreateIndex("dbo.Topics", "Course_Id");
            CreateIndex("dbo.Topics", "Id");
            CreateIndex("dbo.StudentAnswers", "QuizOption_Id");
            CreateIndex("dbo.QuizOptions", "Quiz_Id");
            CreateIndex("dbo.Quizs", "Course_Id");
            CreateIndex("dbo.Comments", "Comment_Id");
            CreateIndex("dbo.Comments", "Feed_Id");
            AddForeignKey("dbo.StudentAnswers", "QuizOption_Id", "dbo.QuizOptions", "Id");
            AddForeignKey("dbo.QuizOptions", "Quiz_Id", "dbo.Quizs", "Id");
            AddForeignKey("dbo.Videos", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Topics", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Quizs", "Course_Id", "dbo.Courses", "Id");
            AddForeignKey("dbo.Videos", "Id", "dbo.Feeds", "Id");
            AddForeignKey("dbo.Topics", "Id", "dbo.Feeds", "Id");
            AddForeignKey("dbo.Comments", "Feed_Id", "dbo.Feeds", "Id");
            AddForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments", "Id");
            RenameTable(name: "dbo.AspNetUsers", newName: "Users");
        }
    }
}
