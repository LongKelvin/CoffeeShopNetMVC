namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddMoreEntitiesToOrderTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderStatus", c => c.Int(nullable: false));
            AddColumn("dbo.Orders", "ShippingStatus", c => c.Int(nullable: false));
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Orders", "PaymentStatus", c => c.String(nullable: false, maxLength: 50));
            DropColumn("dbo.Orders", "ShippingStatus");
            DropColumn("dbo.Orders", "OrderStatus");
        }
    }
}
