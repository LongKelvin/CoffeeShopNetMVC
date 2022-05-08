namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modifySlideTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Slide", "ActionName", c => c.String(nullable: false, maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Slide", "ActionName");
        }
    }
}
