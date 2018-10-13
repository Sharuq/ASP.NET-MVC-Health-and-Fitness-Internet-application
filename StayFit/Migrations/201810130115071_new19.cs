namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new19 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.GymMembers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GymMembers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.GymMembers", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.GymMembers", "ApplicationUser_Id");
            AddForeignKey("dbo.GymMembers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.GymMembers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GymMembers", new[] { "ApplicationUser_Id" });
            AlterColumn("dbo.GymMembers", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.GymMembers", "ApplicationUser_Id");
            AddForeignKey("dbo.GymMembers", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
