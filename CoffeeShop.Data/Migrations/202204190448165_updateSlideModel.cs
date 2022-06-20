namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class updateSlideModel : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Slide", "DisplayOrder", c => c.Int(nullable: false));
        }

        public override void Down()
        {
            AlterColumn("dbo.Slide", "DisplayOrder", c => c.Int());
        }
    }
}