namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAppUserPermissionPrimaryKey : DbMigration
    {
        public override void Up()
        {
            DropPrimaryKey("dbo.ApplicationUserPermissions");
            AddPrimaryKey("dbo.ApplicationUserPermissions", new[] { "UserId", "PermissionId", "RoleId" });
        }
        
        public override void Down()
        {
            DropPrimaryKey("dbo.ApplicationUserPermissions");
            AddPrimaryKey("dbo.ApplicationUserPermissions", new[] { "UserId", "PermissionId" });
        }
    }
}
