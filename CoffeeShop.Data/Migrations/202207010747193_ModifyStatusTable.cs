namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ModifyStatusTable : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.OrderStatus");
            DropPrimaryKey("dbo.PaymentStatus");
            DropPrimaryKey("dbo.ShippingStatus");
            AlterColumn("dbo.OrderStatus", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.PaymentStatus", "ID", c => c.Int(nullable: false));
            AlterColumn("dbo.ShippingStatus", "ID", c => c.Int(nullable: false));
            AddPrimaryKey("dbo.OrderStatus", "ID");
            AddPrimaryKey("dbo.PaymentStatus", "ID");
            AddPrimaryKey("dbo.ShippingStatus", "ID");
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ShippingStatus");
            DropPrimaryKey("dbo.PaymentStatus");
            DropPrimaryKey("dbo.OrderStatus");
            AlterColumn("dbo.ShippingStatus", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.PaymentStatus", "ID", c => c.Int(nullable: false, identity: true));
            AlterColumn("dbo.OrderStatus", "ID", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.ShippingStatus", "ID");
            AddPrimaryKey("dbo.PaymentStatus", "ID");
            AddPrimaryKey("dbo.OrderStatus", "ID");
        }
    }
}
