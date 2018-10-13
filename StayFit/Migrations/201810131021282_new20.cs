namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new20 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PostMessages",
                c => new
                    {
                        Post_Message_Id = c.Int(nullable: false, identity: true),
                        Post_Message = c.String(nullable: false),
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        Post_Post_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Post_Message_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.Posts", t => t.Post_Post_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Post_Post_Id);
            
            CreateTable(
                "dbo.Posts",
                c => new
                    {
                        Post_Id = c.Int(nullable: false, identity: true),
                        Post_Title = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.Post_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostMessages", "Post_Post_Id", "dbo.Posts");
            DropForeignKey("dbo.PostMessages", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.PostMessages", new[] { "Post_Post_Id" });
            DropIndex("dbo.PostMessages", new[] { "ApplicationUser_Id" });
            DropTable("dbo.Posts");
            DropTable("dbo.PostMessages");
        }
    }
}
