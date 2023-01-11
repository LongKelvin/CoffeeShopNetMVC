namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class updateorderDetaislModel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.OrderDetails", "ProductImage", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.OrderDetails", "ProductImage");
        }
    }
}
