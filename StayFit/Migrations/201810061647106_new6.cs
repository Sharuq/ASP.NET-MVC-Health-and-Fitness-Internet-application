namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new152 : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ServiceBookings", "ServiceTime");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceBookings", "ServiceTime", c => c.String(maxLength: 50));
        }
    }
}
