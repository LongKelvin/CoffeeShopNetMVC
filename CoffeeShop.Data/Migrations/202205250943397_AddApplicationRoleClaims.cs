namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationRoleClaims : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationRoleClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        RoleId = c.String(maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ApplicationRoles", t => t.RoleId)
                .Index(t => t.RoleId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationRoleClaims", "RoleId", "dbo.ApplicationRoles");
            DropIndex("dbo.ApplicationRoleClaims", new[] { "RoleId" });
            DropTable("dbo.ApplicationRoleClaims");
        }
    }
}
