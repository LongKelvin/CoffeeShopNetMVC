namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateAppPermission : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions");
            DropForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions");
            DropPrimaryKey("dbo.ApplicationPermissions");
            AlterColumn("dbo.ApplicationPermissions", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ApplicationPermissions", "Id");
            AddForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions");
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions");
            DropPrimaryKey("dbo.ApplicationPermissions");
            AlterColumn("dbo.ApplicationPermissions", "Id", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ApplicationPermissions", "Id");
            AddForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions", "Id", cascadeDelete: true);
        }
    }
}
