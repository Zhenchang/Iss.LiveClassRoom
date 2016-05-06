namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addDatesToQuiz : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Quizs", "StartDate", c => c.DateTime(nullable: false));
            AddColumn("dbo.Quizs", "EndDate", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Quizs", "EndDate");
            DropColumn("dbo.Quizs", "StartDate");
        }
    }
}
