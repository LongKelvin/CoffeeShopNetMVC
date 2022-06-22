namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddNewAppUserColumn : DbMigration
    {
        public override void Up()
        {
            RenameColumn("dbo.ApplicationUsers", "AdminAccessPermission", "AdminAccessPermission");
        }

        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "AdminAccessPermission");
        }
    }
}