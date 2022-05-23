namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class modify_User_Order_tbl : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CustomerId", c => c.String(maxLength: 128));
            AddColumn("dbo.ApplicationUsers", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.ApplicationUsers", "Sex", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "CustomerId");
            AddForeignKey("dbo.Orders", "CustomerId", "dbo.ApplicationUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "CustomerId", "dbo.ApplicationUsers");
            DropIndex("dbo.Orders", new[] { "CustomerId" });
            DropColumn("dbo.ApplicationUsers", "Sex");
            DropColumn("dbo.ApplicationUsers", "CreatedDate");
            DropColumn("dbo.Orders", "CustomerId");
        }
    }
}
