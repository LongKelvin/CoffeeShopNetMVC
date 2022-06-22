namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class AddApplicationPermission : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ApplicationPermissions",
                c => new
                {
                    Id = c.String(nullable: false, maxLength: 128),
                    Name = c.String(nullable: false),
                    Module = c.String(nullable: false),
                    Type = c.String(nullable: false),
                    Description = c.String(nullable: false),
                })
                .PrimaryKey(t => t.Id);
        }

        public override void Down()
        {
            DropTable("dbo.ApplicationPermissions");
        }
    }
}