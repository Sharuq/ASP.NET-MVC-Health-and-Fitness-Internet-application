namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new18 : DbMigration
    {
        public override void Up()
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
                        ServiceTimings_Timing_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Booking_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Services", t => t.Service_Service_Id)
                .ForeignKey("dbo.ServiceTimings", t => t.ServiceTimings_Timing_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Service_Service_Id)
                .Index(t => t.ServiceTimings_Timing_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceBookings", "ServiceTimings_Timing_Id", "dbo.ServiceTimings");
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceBookings", new[] { "ServiceTimings_Timing_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ServiceBookings");
        }
    }
}
