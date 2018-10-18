namespace StayFit.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class new21 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.PaymentGateways");
        }
        
        public override void Down()
        {
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
            
        }
    }
}
