namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new24 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Images",
                c => new
                    {
                        Image_Id = c.Int(nullable: false, identity: true),
                        Image_Path = c.String(nullable: false, maxLength: 70),
                    })
                .PrimaryKey(t => t.Image_Id);
            
            AddColumn("dbo.PostMessages", "Image_Image_Id", c => c.Int());
            CreateIndex("dbo.PostMessages", "Image_Image_Id");
            AddForeignKey("dbo.PostMessages", "Image_Image_Id", "dbo.Images", "Image_Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PostMessages", "Image_Image_Id", "dbo.Images");
            DropIndex("dbo.PostMessages", new[] { "Image_Image_Id" });
            DropColumn("dbo.PostMessages", "Image_Image_Id");
            DropTable("dbo.Images");
        }
    }
}
