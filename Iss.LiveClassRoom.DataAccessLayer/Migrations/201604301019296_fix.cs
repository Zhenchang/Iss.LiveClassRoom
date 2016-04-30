namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class fix : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Topics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                        Content = c.String(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                        Title = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
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
                        Instructor_Id = c.String(maxLength: 32),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: null, newValue: "IsDeleted")
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: null, newValue: "TimeDeletedUtc")
                    },
                });
            
        }
        
        public override void Down()
        {
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Students",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Instructors",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
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
                        Instructor_Id = c.String(maxLength: 32),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Videos",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                        Title = c.String(nullable: false),
                        FileName = c.String(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
                "dbo.Topics",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 32),
                        Course_Id = c.String(nullable: false, maxLength: 32),
                        Content = c.String(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
            AlterTableAnnotations(
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
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "SoftDeleteColumnName",
                        new AnnotationValues(oldValue: "IsDeleted", newValue: null)
                    },
                    { 
                        "SoftDeleteDateColumnName",
                        new AnnotationValues(oldValue: "TimeDeletedUtc", newValue: null)
                    },
                });
            
        }
    }
}
