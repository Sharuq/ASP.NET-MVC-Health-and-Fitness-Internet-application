namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _new : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ServiceBookings");
            DropTable("dbo.Services");

        }
        
        public override void Down()
        {
           
        }
    }
}
