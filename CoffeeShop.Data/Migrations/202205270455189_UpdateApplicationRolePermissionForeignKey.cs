namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class UpdateApplicationRolePermissionForeignKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.ApplicationRolePermissions", name: "RoleId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ApplicationRolePermissions", name: "PermissionId", newName: "RoleId");
            RenameColumn(table: "dbo.ApplicationRolePermissions", name: "__mig_tmp__0", newName: "PermissionId");
            RenameIndex(table: "dbo.ApplicationRolePermissions", name: "IX_PermissionId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ApplicationRolePermissions", name: "IX_RoleId", newName: "IX_PermissionId");
            RenameIndex(table: "dbo.ApplicationRolePermissions", name: "__mig_tmp__0", newName: "IX_RoleId");
        }

        public override void Down()
        {
            RenameIndex(table: "dbo.ApplicationRolePermissions", name: "IX_RoleId", newName: "__mig_tmp__0");
            RenameIndex(table: "dbo.ApplicationRolePermissions", name: "IX_PermissionId", newName: "IX_RoleId");
            RenameIndex(table: "dbo.ApplicationRolePermissions", name: "__mig_tmp__0", newName: "IX_PermissionId");
            RenameColumn(table: "dbo.ApplicationRolePermissions", name: "PermissionId", newName: "__mig_tmp__0");
            RenameColumn(table: "dbo.ApplicationRolePermissions", name: "RoleId", newName: "PermissionId");
            RenameColumn(table: "dbo.ApplicationRolePermissions", name: "__mig_tmp__0", newName: "RoleId");
        }
    }
}