namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new27 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            AlterColumn("dbo.ServiceBookings", "Service_Service_Id", c => c.Int(nullable: false));
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceBookings", new[] { "Service_Service_Id" });
            AlterColumn("dbo.ServiceBookings", "Service_Service_Id", c => c.Int());
            CreateIndex("dbo.ServiceBookings", "Service_Service_Id");
            AddForeignKey("dbo.ServiceBookings", "Service_Service_Id", "dbo.Services", "Service_Id");
        }
    }
}
