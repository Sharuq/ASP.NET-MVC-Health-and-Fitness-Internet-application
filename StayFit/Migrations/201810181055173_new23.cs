namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new23 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Images", "PostMessage_Post_Message_Id", "dbo.PostMessages");
            DropIndex("dbo.Images", new[] { "PostMessage_Post_Message_Id" });
            DropTable("dbo.Images");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Image_Id = c.Int(nullable: false, identity: true),
                        Image_Path = c.String(nullable: false, maxLength: 70),
                        PostMessage_Post_Message_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Image_Id);
            
            CreateIndex("dbo.Images", "PostMessage_Post_Message_Id");
            AddForeignKey("dbo.Images", "PostMessage_Post_Message_Id", "dbo.PostMessages", "Post_Message_Id");
        }
    }
}
