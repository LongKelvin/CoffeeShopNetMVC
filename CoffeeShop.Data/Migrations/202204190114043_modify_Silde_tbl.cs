namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class modify_Silde_tbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slide", "Title", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Slide", "Name");
        }

        public override void Down()
        {
            AddColumn("dbo.Slide", "Name", c => c.String(nullable: false, maxLength: 250));
            DropColumn("dbo.Slide", "Title");
        }
    }
}