namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new112 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ServiceBookings", "ServiceTime", c => c.String(maxLength: 50));
            DropColumn("dbo.ServiceBookings", "SeviceName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.ServiceBookings", "SeviceName", c => c.String(maxLength: 50));
            DropColumn("dbo.ServiceBookings", "ServiceTime");
        }
    }
}
