namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new121 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceBookings", "Service_Service_Id", c => c.Int());
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropColumn("dbo.ServiceBookings", "Service_Service_Id");
        }
    }
}
