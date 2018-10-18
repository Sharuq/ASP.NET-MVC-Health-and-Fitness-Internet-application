namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new22 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Image_Id = c.Int(nullable: false, identity: true),
                        Image_Path = c.String(nullable: false, maxLength: 70),
                        PostMessage_Post_Message_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Image_Id)
                .ForeignKey("dbo.PostMessages", t => t.PostMessage_Post_Message_Id)
                .Index(t => t.PostMessage_Post_Message_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Images", "PostMessage_Post_Message_Id", "dbo.PostMessages");
            DropIndex("dbo.Images", new[] { "PostMessage_Post_Message_Id" });
            DropTable("dbo.Images");
        }
    }
}
