namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addAliasColToTblPage : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pages", "Alias", c => c.String(maxLength: 250));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pages", "Alias");
        }
    }
}
