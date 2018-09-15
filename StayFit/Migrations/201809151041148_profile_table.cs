namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class profile_table : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.MemberProfiles",
                c => new
                    {
                        member_Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 50),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.member_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.MemberProfiles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MemberProfiles", new[] { "ApplicationUser_Id" });
            DropTable("dbo.MemberProfiles");
        }
    }
}
