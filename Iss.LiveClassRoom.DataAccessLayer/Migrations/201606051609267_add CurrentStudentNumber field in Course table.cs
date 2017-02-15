namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addCurrentStudentNumberfieldinCoursetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "CurrentStudentNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "CurrentStudentNumber");
        }
    }
}
