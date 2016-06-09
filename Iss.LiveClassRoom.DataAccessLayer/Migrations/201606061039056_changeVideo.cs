namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeVideo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "IsAccept", c => c.Boolean(nullable: false));
            AddColumn("dbo.Videos", "Comment", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "Comment");
            DropColumn("dbo.Videos", "IsAccept");
        }
    }
}
