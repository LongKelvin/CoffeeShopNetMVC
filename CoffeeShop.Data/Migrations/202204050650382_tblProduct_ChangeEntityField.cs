namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class tblProduct_ChangeEntityField : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "PromotionPrice", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Products", "Promotion");
        }

        public override void Down()
        {
            AddColumn("dbo.Products", "Promotion", c => c.Decimal(precision: 18, scale: 2));
            DropColumn("dbo.Products", "PromotionPrice");
        }
    }
}