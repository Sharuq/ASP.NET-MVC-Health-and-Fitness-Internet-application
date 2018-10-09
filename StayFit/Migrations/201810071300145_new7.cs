namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new7 : DbMigration
    {
        public override void Up()
        {
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
            DropForeignKey("dbo.ServiceTimings", "Service_Service_Id", "dbo.Services");
            DropIndex("dbo.ServiceTimings", new[] { "Service_Service_Id" });
            DropTable("dbo.ServiceTimings");
        }
    }
}
