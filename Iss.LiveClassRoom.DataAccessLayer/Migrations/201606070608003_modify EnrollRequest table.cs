namespace Iss.LiveClassRoom.DataAccessLayer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyEnrollRequesttable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EnrollmentRequests", "InstanceId", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.EnrollmentRequests", "InstanceId");
        }
    }
}
