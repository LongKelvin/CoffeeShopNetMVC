namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifyOrderTbl : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Orders", name: "PaymentMethodCode", newName: "PaymentMethodID");
            RenameIndex(table: "dbo.Orders", name: "IX_PaymentMethodCode", newName: "IX_PaymentMethodID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Orders", name: "IX_PaymentMethodID", newName: "IX_PaymentMethodCode");
            RenameColumn(table: "dbo.Orders", name: "PaymentMethodID", newName: "PaymentMethodCode");
        }
    }
}
