namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtraTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.OrderStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusCode = c.Int(nullable: false),
                        StatusName = c.String(nullable: false),
                        StatusDescription = c.String(),
                        IsCanDelete = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.PaymentStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusCode = c.Int(nullable: false),
                        StatusName = c.String(nullable: false),
                        StatusDescription = c.String(),
                        IsCanDelete = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ShippingStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        StatusCode = c.Int(nullable: false),
                        StatusName = c.String(nullable: false),
                        StatusDescription = c.String(),
                        IsCanDelete = c.Boolean(nullable: false),
                        IsActive = c.Boolean(nullable: false),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ShippingStatus");
            DropTable("dbo.PaymentStatus");
            DropTable("dbo.OrderStatus");
        }
    }
}
