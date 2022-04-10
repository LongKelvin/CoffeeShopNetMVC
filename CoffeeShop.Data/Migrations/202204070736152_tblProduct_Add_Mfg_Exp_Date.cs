namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class tblProduct_Add_Mfg_Exp_Date : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ManufacturingDate", c => c.DateTime());
            AddColumn("dbo.Products", "ExpireDate", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ExpireDate");
            DropColumn("dbo.Products", "ManufacturingDate");
        }
    }
}
