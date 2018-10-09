namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new13 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceTimings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            DropIndex("dbo.ServiceTimings", new[] { "Service_Service_Id" });
            DropTable("dbo.Services");
            DropTable("dbo.ServiceBookings");
            DropTable("dbo.ServiceTimings");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ServiceTimings",
                c => new
                    {
                        Timing_Id = c.Int(nullable: false, identity: true),
                        Timing = c.String(nullable: false),
                        Service_Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Timing_Id);
            
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
            
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Service_Id = c.Int(nullable: false, identity: true),
                        SeviceName = c.String(nullable: false, maxLength: 50),
                        ServiceDesc = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.Service_Id);
            
            CreateIndex("dbo.ServiceTimings", "Service_Service_Id");
            CreateIndex("dbo.ServiceBookings", "ServiceTimings_Timing_Id");
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            CreateIndex("dbo.ServiceBookings", "ApplicationUser_Id");
            AddForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings", "Timing_Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceTimings", "Service_Service_Id", "dbo.Services", "Service_Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
            AddForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
