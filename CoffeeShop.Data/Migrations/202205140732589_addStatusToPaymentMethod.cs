namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addStatusToPaymentMethod : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.PaymentMethod", "Status", c => c.Boolean(nullable: false));
        }

        public override void Down()
        {
            DropColumn("dbo.PaymentMethod", "Status");
        }
    }
}