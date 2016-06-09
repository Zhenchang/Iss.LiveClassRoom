namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class changeIsacceptInVideo : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Videos", "IsAccept", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Videos", "IsAccept", c => c.Boolean(nullable: false));
        }
    }
}
