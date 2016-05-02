namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addTitleToTopic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Topics", "Title", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Topics", "Title");
        }
    }
}
