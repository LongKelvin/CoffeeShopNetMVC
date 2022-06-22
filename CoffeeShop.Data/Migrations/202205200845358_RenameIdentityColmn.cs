namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class RenameIdentityColmn : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.ApplicationUserRoles", "ApplicationUser_Id");
            DropColumn("dbo.ApplicationUserRoles", "IdentityRole_Id");
        }

        public override void Down()
        {
            AddColumn("dbo.ApplicationUserRoles", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationUserRoles", "IdentityRole_Id", c => c.String(nullable: false, maxLength: 128));
        }
    }
}