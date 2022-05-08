namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddShopInfo : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ShopInfo",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Logo = c.String(),
                        Address = c.String(nullable: false),
                        Address2 = c.String(),
                        Telephone = c.String(nullable: false),
                        Mobiphone1 = c.String(),
                        Mobiphone2 = c.String(),
                        Email = c.String(nullable: false),
                        Email2 = c.String(),
                        Status = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ShopPayment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        BankName = c.String(nullable: false),
                        BankType = c.String(),
                        CardNumner = c.String(nullable: false),
                        CardNumLimit = c.Int(nullable: false),
                        AccountName = c.String(nullable: false),
                        CardPhone = c.String(),
                        Status = c.String(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShopPayment");
            DropTable("dbo.ShopInfo");
        }
    }
}
