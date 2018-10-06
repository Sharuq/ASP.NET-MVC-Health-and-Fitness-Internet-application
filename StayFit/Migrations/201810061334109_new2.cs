namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            AddColumn("dbo.ServiceBookings", "SeviceName", c => c.String(maxLength: 50));
            AlterColumn("dbo.ServiceBookings", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            
            CreateIndex("dbo.ServiceBookings", "ApplicationUser_Id");
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            AddForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id", cascadeDelete: true);
            DropColumn("dbo.ServiceBookings", "ServiceTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceBookings", "ServiceTime", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            DropIndex("dbo.ServiceBookings", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.ServiceBookings", "Service_Service_Id", c => c.Int());
            AlterColumn("dbo.ServiceBookings", "ApplicationUser_Id", c => c.String(maxLength: 128));
            DropColumn("dbo.ServiceBookings", "SeviceName");
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            CreateIndex("dbo.ServiceBookings", "ApplicationUser_Id");
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
            AddForeignKey("dbo.ServiceBookings", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
