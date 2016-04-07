namespace Iss.LiveClassRoom.Core.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFileNameForVideo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Videos", "FileName", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Videos", "FileName");
        }
    }
}
