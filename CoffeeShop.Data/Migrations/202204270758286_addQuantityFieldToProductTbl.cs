namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addQuantityFieldToProductTbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Quantity", c => c.Int(nullable: false));
            Sql("UPDATE dbo.Products SET Quantity = 0");
        }

        public override void Down()
        {
            DropColumn("dbo.Products", "Quantity");
        }
    }
}