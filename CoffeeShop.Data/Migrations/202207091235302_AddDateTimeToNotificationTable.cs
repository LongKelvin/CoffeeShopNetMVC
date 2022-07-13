namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class AddDateTimeToNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationNotifications", "DateCreated", c => c.DateTime(nullable: false, defaultValueSql: "GETDATE()"));
        }

        public override void Down()
        {
            DropColumn("dbo.ApplicationNotifications", "DateCreated");
        }
    }
}
