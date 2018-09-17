namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class newfeatures : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.MemberProfiles", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.MemberProfiles", new[] { "ApplicationUser_Id" });
            CreateTable(
                "dbo.GymMembers",
                c => new
                    {
                        Member_Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false, maxLength: 50),
                        LastName = c.String(nullable: false, maxLength: 50),
                        DateOfBirth = c.DateTime(nullable: false),
                        Address = c.String(nullable: false, maxLength: 90),
                        Height = c.Int(nullable: false),
                        Weight = c.Int(nullable: false),
                        MembershipType = c.String(nullable: false),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Member_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.MembershipTypes",
                c => new
                    {
                        Membership_Id = c.Int(nullable: false, identity: true),
                        Membership_tier = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Membership_Id);
            
            CreateTable(
                "dbo.PaymentGateways",
                c => new
                    {
                        Payment_Id = c.Int(nullable: false, identity: true),
                        NameOnCard = c.String(nullable: false, maxLength: 80),
                        CardNumber = c.Int(nullable: false),
                        CVV = c.Int(nullable: false),
                        ExpiryDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Payment_Id);
            
            DropTable("dbo.MemberProfiles");
        }
        
        public override void Down()
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
                .PrimaryKey(t => t.member_Id);
            
            DropForeignKey("dbo.GymMembers", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.GymMembers", new[] { "ApplicationUser_Id" });
            DropTable("dbo.PaymentGateways");
            DropTable("dbo.MembershipTypes");
            DropTable("dbo.GymMembers");
            CreateIndex("dbo.MemberProfiles", "ApplicationUser_Id");
            AddForeignKey("dbo.MemberProfiles", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
