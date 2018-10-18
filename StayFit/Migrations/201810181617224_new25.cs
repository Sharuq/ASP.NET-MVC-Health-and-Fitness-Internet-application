namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new25 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PostMessages", "Approval_Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.PostMessages", "Approval_Status");
        }
    }
}
