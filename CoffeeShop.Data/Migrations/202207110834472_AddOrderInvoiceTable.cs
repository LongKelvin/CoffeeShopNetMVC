namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddOrderInvoiceTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderInvoices",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        InvoiceCode = c.String(),
                        CreatedDate = c.DateTime(nullable: false),
                        Cashier = c.String(),
                        OrderId = c.Int(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Orders", t => t.OrderId, cascadeDelete: true)
                .Index(t => t.OrderId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OrderInvoices", "OrderId", "dbo.Orders");
            DropIndex("dbo.OrderInvoices", new[] { "OrderId" });
            DropTable("dbo.OrderInvoices");
        }
    }
}
