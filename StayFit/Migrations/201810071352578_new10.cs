namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new10 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            AlterColumn("dbo.ServiceBookings", "ServiceTimings_Timing_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceBookings", "ServiceTimings_Timing_Id");
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings", "Timing_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            AlterColumn("dbo.ServiceBookings", "ServiceTimings_Timing_Id", c => c.Int());
            CreateIndex("dbo.ServiceBookings", "ServiceTimings_Timing_Id");
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings", "Timing_Id");
        }
    }
}
