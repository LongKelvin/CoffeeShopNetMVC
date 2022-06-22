namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateApplicationPermissionId : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions");
            DropForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions");
            DropPrimaryKey("dbo.ApplicationPermissions");
            AddColumn("dbo.ApplicationPermissions", "PermissionId", c => c.String(nullable: false, maxLength: 128));
            AddPrimaryKey("dbo.ApplicationPermissions", "PermissionId");
            AddForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions", "PermissionId", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions", "PermissionId", cascadeDelete: true);
            DropColumn("dbo.ApplicationPermissions", "ID");
        }

        public override void Down()
        {
            AddColumn("dbo.ApplicationPermissions", "ID", c => c.String(nullable: false, maxLength: 128));
            DropForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions");
            DropForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions");
            DropPrimaryKey("dbo.ApplicationPermissions");
            DropColumn("dbo.ApplicationPermissions", "PermissionId");
            AddPrimaryKey("dbo.ApplicationPermissions", "ID");
            AddForeignKey("dbo.ApplicationUserPermissions", "PermissionId", "dbo.ApplicationPermissions", "ID", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationRolePermissions", "RoleId", "dbo.ApplicationPermissions", "ID", cascadeDelete: true);
        }
    }
}