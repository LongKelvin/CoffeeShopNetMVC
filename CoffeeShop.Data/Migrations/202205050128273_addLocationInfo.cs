namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addLocationInfo : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ShopInfo", "Code", c => c.String());
            AddColumn("dbo.ShopInfo", "MobilePhone1", c => c.String());
            AddColumn("dbo.ShopInfo", "MobilePhone2", c => c.String());
            AddColumn("dbo.ShopInfo", "Website", c => c.String());
            AddColumn("dbo.ShopInfo", "Latitude", c => c.Double(nullable: false));
            AddColumn("dbo.ShopInfo", "Longitude", c => c.Double(nullable: false));
            AddColumn("dbo.ShopInfo", "Other", c => c.String());
            DropColumn("dbo.ShopInfo", "Mobiphone1");
            DropColumn("dbo.ShopInfo", "Mobiphone2");
        }

        public override void Down()
        {
            AddColumn("dbo.ShopInfo", "Mobiphone2", c => c.String());
            AddColumn("dbo.ShopInfo", "Mobiphone1", c => c.String());
            DropColumn("dbo.ShopInfo", "Other");
            DropColumn("dbo.ShopInfo", "Longitude");
            DropColumn("dbo.ShopInfo", "Latitude");
            DropColumn("dbo.ShopInfo", "Website");
            DropColumn("dbo.ShopInfo", "MobilePhone2");
            DropColumn("dbo.ShopInfo", "MobilePhone1");
            DropColumn("dbo.ShopInfo", "Code");
        }
    }
}