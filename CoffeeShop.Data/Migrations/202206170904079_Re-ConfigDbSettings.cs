namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ReConfigDbSettings : DbMigration
    {
        public override void Up()
        {
            //DropColumn("dbo.ApplicationUsers", "AdminPageScope");
        }

        public override void Down()
        {
            //AddColumn("dbo.ApplicationUsers", "AdminPageScope", c => c.Boolean(nullable: false));
        }
    }
}