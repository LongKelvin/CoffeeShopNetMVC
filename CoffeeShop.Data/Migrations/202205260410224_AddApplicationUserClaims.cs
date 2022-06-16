namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddApplicationUserClaims : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ApplicationUserClaims", newName: "IdentityUserClaims");
            DropPrimaryKey("dbo.IdentityUserClaims");
            CreateTable(
                "dbo.ApplicationUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityUserClaims", t => t.Id)
                .Index(t => t.Id);
            
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String());
            AlterColumn("dbo.IdentityUserClaims", "Id", c => c.Int(nullable: false, identity: true));
            AddPrimaryKey("dbo.IdentityUserClaims", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ApplicationUserClaims", "Id", "dbo.IdentityUserClaims");
            DropIndex("dbo.ApplicationUserClaims", new[] { "Id" });
            DropPrimaryKey("dbo.IdentityUserClaims");
            AlterColumn("dbo.IdentityUserClaims", "Id", c => c.Int(nullable: false));
            AlterColumn("dbo.IdentityUserClaims", "UserId", c => c.String(nullable: false, maxLength: 128));
            DropTable("dbo.ApplicationUserClaims");
            AddPrimaryKey("dbo.IdentityUserClaims", "UserId");
            RenameTable(name: "dbo.IdentityUserClaims", newName: "ApplicationUserClaims");
        }
    }
}
