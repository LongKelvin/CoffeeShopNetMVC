namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addLogoImageToPaymentMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentMethod", "LogoImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.PaymentMethod", "LogoImage");
        }
    }
}
