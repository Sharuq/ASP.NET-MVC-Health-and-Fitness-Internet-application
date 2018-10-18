namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new26 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostMessages", "Message_Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.PostMessages", "Approval_Status");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PostMessages", "Approval_Status", c => c.Boolean(nullable: false));
            DropColumn("dbo.PostMessages", "Message_Status");
        }
    }
}
