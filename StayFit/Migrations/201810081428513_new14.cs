namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new14 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Service_Id = c.Int(nullable: false, identity: true),
                        SeviceName = c.String(nullable: false, maxLength: 50),
                        ServiceDesc = c.String(nullable: false, maxLength: 450),
                    })
                .PrimaryKey(t => t.Service_Id);
            
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
                .PrimaryKey(t => t.Booking_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Service_Id)
                .ForeignKey("dbo.ServiceTimings", t => t.ServiceTimings_Timing_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Service_Service_Id)
                .Index(t => t.ServiceTimings_Timing_Id);
            
            CreateTable(
                "dbo.ServiceTimings",
                c => new
                    {
                        Timing_Id = c.Int(nullable: false, identity: true),
                        Timing = c.String(nullable: false),
                        Service_Service_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Timing_Id)
                .ForeignKey("dbo.Services", t => t.Service_Service_Id, cascadeDelete: true)
                .Index(t => t.Service_Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropForeignKey("dbo.ServiceTimings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceTimings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ServiceTimings");
            DropTable("dbo.ServiceBookings");
            DropTable("dbo.Services");
        }
    }
}
