namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameAppUserColumn : DbMigration
    {
        public override void Up()
        {
            RenameColumn("ApplicationUsers", "AlowAccessAdminPage", "AdminAccessPermission");
        }

        public override void Down()
        {
        }
    }
}