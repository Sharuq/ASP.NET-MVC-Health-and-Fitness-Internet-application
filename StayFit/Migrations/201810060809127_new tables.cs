namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Services",
                c => new
                    {
                        Service_Id = c.Int(nullable: false, identity: true),
                        SeviceName = c.String(nullable: false, maxLength: 50),
                        ServiceDesc = c.String(nullable: false, maxLength: 50),
                    })
                .PrimaryKey(t => t.Service_Id);
            
            CreateTable(
                "dbo.ServiceBookings",
                c => new
                    {
                        Booking_Id = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        Timing = c.DateTime(nullable: false),
                        BookingStatus = c.Boolean(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Service_Service_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Booking_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Services", t => t.Service_Service_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Service_Service_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ServiceBookings");
            DropTable("dbo.Services");
        }
    }
}
