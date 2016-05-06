namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fixCourse : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false, maxLength: 50));
            AlterColumn("dbo.Courses", "Instructor_Id", c => c.String(nullable: false, maxLength: 32));
            CreateIndex("dbo.Courses", "Instructor_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Courses", new[] { "Instructor_Id" });
            AlterColumn("dbo.Courses", "Instructor_Id", c => c.String(maxLength: 32));
            AlterColumn("dbo.Courses", "Name", c => c.String(nullable: false));
            CreateIndex("dbo.Courses", "Instructor_Id");
        }
    }
}
