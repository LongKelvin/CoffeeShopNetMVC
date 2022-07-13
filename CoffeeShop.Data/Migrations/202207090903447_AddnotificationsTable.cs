namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddnotificationsTable : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationNotifications",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Message = c.String(),
                        Url = c.String(),
                        Status = c.Boolean(nullable: false),
                        IsReaded = c.Boolean(nullable: false),
                        Type = c.String(),
                        RowVersion = c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.ApplicationNotifications");
        }
    }
}
