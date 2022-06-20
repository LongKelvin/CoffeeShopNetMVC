namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class createPaymentMethod : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PaymentMethod",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    PaymentName = c.String(nullable: false),
                    PaymentCode = c.Int(nullable: false),
                    RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                })
                .PrimaryKey(t => t.ID);

            AddColumn("dbo.Orders", "PaymentMethodCode", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "PaymentMethodCode");
            AddForeignKey("dbo.Orders", "PaymentMethodCode", "dbo.PaymentMethod", "ID", cascadeDelete: true);
            DropColumn("dbo.Orders", "PaymentMehod");
        }

        public override void Down()
        {
            AddColumn("dbo.Orders", "PaymentMehod", c => c.String(nullable: false, maxLength: 250));
            DropForeignKey("dbo.Orders", "PaymentMethodCode", "dbo.PaymentMethod");
            DropIndex("dbo.Orders", new[] { "PaymentMethodCode" });
            DropColumn("dbo.Orders", "PaymentMethodCode");
            DropTable("dbo.PaymentMethod");
        }
    }
}