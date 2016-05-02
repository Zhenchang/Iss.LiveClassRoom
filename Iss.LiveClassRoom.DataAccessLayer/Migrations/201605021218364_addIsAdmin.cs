namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addIsAdmin : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Instructors", "IsAdmin", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Instructors", "IsAdmin");
        }
    }
}
