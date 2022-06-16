namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeApplicationPermissionIdToIdentity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationPermissions", "RowVersion", c => c.Binary(nullable: false, fixedLength: true, timestamp: true, storeType: "rowversion"));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationPermissions", "RowVersion");
        }
    }
}
