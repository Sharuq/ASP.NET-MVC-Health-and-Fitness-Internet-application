namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new17 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            DropTable("dbo.ServiceBookings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceBookings",
                c => new
                    {
                        Booking_Id = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        BookingStatus = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Service_Service_Id = c.Int(),
                        ServiceTimings_Timing_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Booking_Id);
            
            CreateIndex("dbo.ServiceBookings", "ServiceTimings_Timing_Id");
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            CreateIndex("dbo.ServiceBookings", "ApplicationUser_Id");
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings", "Timing_Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
            AddForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
