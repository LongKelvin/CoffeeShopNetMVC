namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ModifyApplicationUserClaims : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserClaims", "Id", "dbo.IdentityUserClaims");
            DropIndex("dbo.IdentityUserClaims", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserClaims", new[] { "Id" });
            DropPrimaryKey("dbo.ApplicationUserClaims");
            AddColumn("dbo.ApplicationUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ApplicationUserClaims", "ClaimType", c => c.String());
            AddColumn("dbo.ApplicationUserClaims", "ClaimValue", c => c.String());
            AddColumn("dbo.ApplicationUserClaims", "Discriminator", c => c.String(nullable: false, maxLength: 128));
            AddColumn("dbo.ApplicationUserClaims", "ApplicationUser_Id", c => c.String(maxLength: 128));
            AddPrimaryKey("dbo.ApplicationUserClaims", "UserId");
            CreateIndex("dbo.ApplicationUserClaims", "ApplicationUser_Id");
            DropTable("dbo.IdentityUserClaims");
        }

        public override void Down()
        {
            CreateTable(
                "dbo.IdentityUserClaims",
                c => new
                {
                    Id = c.Int(nullable: false, identity: true),
                    UserId = c.String(),
                    ClaimType = c.String(),
                    ClaimValue = c.String(),
                    ApplicationUser_Id = c.String(maxLength: 128),
                })
                .PrimaryKey(t => t.Id);

            DropIndex("dbo.ApplicationUserClaims", new[] { "ApplicationUser_Id" });
            DropPrimaryKey("dbo.ApplicationUserClaims");
            DropColumn("dbo.ApplicationUserClaims", "ApplicationUser_Id");
            DropColumn("dbo.ApplicationUserClaims", "Discriminator");
            DropColumn("dbo.ApplicationUserClaims", "ClaimValue");
            DropColumn("dbo.ApplicationUserClaims", "ClaimType");
            DropColumn("dbo.ApplicationUserClaims", "UserId");
            AddPrimaryKey("dbo.ApplicationUserClaims", "Id");
            CreateIndex("dbo.ApplicationUserClaims", "Id");
            CreateIndex("dbo.IdentityUserClaims", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserClaims", "Id", "dbo.IdentityUserClaims", "Id");
        }
    }
}