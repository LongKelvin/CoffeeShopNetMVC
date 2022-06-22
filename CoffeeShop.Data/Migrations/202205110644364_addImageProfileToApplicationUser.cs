namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class addImageProfileToApplicationUser : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationUsers", "ProfileImage", c => c.String());
        }

        public override void Down()
        {
            DropColumn("dbo.ApplicationUsers", "ProfileImage");
        }
    }
}