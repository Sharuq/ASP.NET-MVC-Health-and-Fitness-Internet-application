namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new8 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Service_Id", "dbo.ServiceTimings");
            RenameColumn(table: "dbo.ServiceBookings", name: "ServiceTimings_Service_Id", newName: "ServiceTimings_Timing_Id");
            RenameIndex(table: "dbo.ServiceBookings", name: "IX_ServiceTimings_Service_Id", newName: "IX_ServiceTimings_Timing_Id");
            
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings", "Timing_Id");
           
        }

        public override void Down()
        {
            AddColumn("dbo.ServiceTimings", "Service_Id", c => c.Int(nullable: false, identity: true));
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropPrimaryKey("dbo.ServiceTimings");
            DropColumn("dbo.ServiceTimings", "Timing_Id");
            AddPrimaryKey("dbo.ServiceTimings", "Service_Id");
            RenameIndex(table: "dbo.ServiceBookings", name: "IX_ServiceTimings_Timing_Id", newName: "IX_ServiceTimings_Service_Id");
            RenameColumn(table: "dbo.ServiceBookings", name: "ServiceTimings_Timing_Id", newName: "ServiceTimings_Service_Id");
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Service_Id", "dbo.ServiceTimings", "Service_Id");
        }
    }
}
