﻿namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddAdditionalFieldToOrderAndProduct : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "UnitPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AddColumn("dbo.Products", "OriginalPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }

        public override void Down()
        {
            DropColumn("dbo.Products", "OriginalPrice");
            DropColumn("dbo.OrderDetails", "UnitPrice");
        }
    }
}