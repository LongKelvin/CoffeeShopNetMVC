namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateApplicationPermission : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationPermissions", "IsSystemProtected", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationPermissions", "IsSystemProtected");
        }
    }
}
