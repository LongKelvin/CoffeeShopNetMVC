namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateChanges : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderInvoices", "Status", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderInvoices", "Status");
        }
    }
}
