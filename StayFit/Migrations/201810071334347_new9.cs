namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new9 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropColumn("dbo.ServiceBookings", "Service_Service_Id");
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            DropColumn("dbo.ServiceBookings", "ServiceTimings_Timing_Id");
            AddColumn("dbo.ServiceBookings", "ServiceTimings_Timing_Id", c => c.Int());
            CreateIndex("dbo.ServiceBookings", "ServiceTimings_Timing_Id");
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings", "Timing_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceBookings", "Service_Service_Id", c => c.Int());
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
        }
    }
}
