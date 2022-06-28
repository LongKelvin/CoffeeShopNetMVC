namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtraFieldToOrderDetail : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductName", c => c.String());
            AddColumn("dbo.OrderDetails", "TotalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "TotalPrice");
            DropColumn("dbo.OrderDetails", "ProductName");
        }
    }
}
