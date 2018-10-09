namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new15 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bookingviewmodels",
                c => new
                    {
                        Booking_Id = c.Int(nullable: false, identity: true),
                        BookingDate = c.DateTime(nullable: false),
                        BookingStatus = c.Boolean(nullable: false),
                        service_Id = c.Int(nullable: false),
                        timing_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Booking_Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Bookingviewmodels");
        }
    }
}
