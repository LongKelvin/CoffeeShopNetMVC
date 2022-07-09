namespace CoffeeShop.Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddExtraValueToNotificationTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ApplicationNotifications", "ExtraValue", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.ApplicationNotifications", "ExtraValue");
        }
    }
}
