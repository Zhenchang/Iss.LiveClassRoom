namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class deleteReplay : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments");
            DropIndex("dbo.Comments", new[] { "Comment_Id" });
            DropColumn("dbo.Comments", "Discriminator");
            DropColumn("dbo.Comments", "Comment_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Comments", "Comment_Id", c => c.String(maxLength: 32));
            AddColumn("dbo.Comments", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Comments", "Comment_Id");
            AddForeignKey("dbo.Comments", "Comment_Id", "dbo.Comments", "Id");
        }
    }
}
