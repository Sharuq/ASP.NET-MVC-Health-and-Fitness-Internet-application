namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newtables1 : DbMigration
    {
        public override void Up()
        {
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

            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            CreateIndex("dbo.ServiceBookings", "ApplicationUser_Id");
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
            AddForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
           
        }
    }
}
