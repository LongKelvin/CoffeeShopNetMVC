namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddNewFieldToProductModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "TotalAmount", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "TotalItemPrice", c => c.Decimal(precision: 18, scale: 2));
            AddColumn("dbo.Orders", "ShippingFee", c => c.Decimal(precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Orders", "ShippingFee");
            DropColumn("dbo.Orders", "TotalItemPrice");
            DropColumn("dbo.Orders", "TotalAmount");
        }
    }
}
