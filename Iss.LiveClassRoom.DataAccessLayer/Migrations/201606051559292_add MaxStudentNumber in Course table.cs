namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addMaxStudentNumberinCoursetable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Courses", "MaxStudentNumber", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Courses", "MaxStudentNumber");
        }
    }
}
