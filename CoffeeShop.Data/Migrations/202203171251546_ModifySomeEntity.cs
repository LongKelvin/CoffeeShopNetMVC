namespace CoffeeShop.Data.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class ModifySomeEntity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Orders", "CreatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Products", "CreatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Products", "UpdatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductCategories", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "CreatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductCategories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Posts", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.Posts", "CreatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Posts", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdatedBy", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.PostCategories", "CreatedDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "CreatedBy", c => c.String(maxLength: 50));
            AddColumn("dbo.PostCategories", "UpdatedDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "UpdatedBy", c => c.String(maxLength: 50));
            DropColumn("dbo.Orders", "CreateDate");
            DropColumn("dbo.Orders", "CreateBy");
            DropColumn("dbo.Products", "CreateDate");
            DropColumn("dbo.Products", "CreateBy");
            DropColumn("dbo.Products", "UpdateDate");
            DropColumn("dbo.Products", "UpdateBy");
            DropColumn("dbo.ProductCategories", "CreateDate");
            DropColumn("dbo.ProductCategories", "CreateBy");
            DropColumn("dbo.ProductCategories", "UpdateDate");
            DropColumn("dbo.ProductCategories", "UpdateBy");
            DropColumn("dbo.Posts", "CreateDate");
            DropColumn("dbo.Posts", "CreateBy");
            DropColumn("dbo.Posts", "UpdateDate");
            DropColumn("dbo.Posts", "UpdateBy");
            DropColumn("dbo.PostCategories", "CreateDate");
            DropColumn("dbo.PostCategories", "CreateBy");
            DropColumn("dbo.PostCategories", "UpdateDate");
            DropColumn("dbo.PostCategories", "UpdateBy");
        }

        public override void Down()
        {
            AddColumn("dbo.PostCategories", "UpdateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.PostCategories", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.PostCategories", "CreateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.PostCategories", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Posts", "UpdateBy", c => c.String(nullable: false, maxLength: 50));
            AddColumn("dbo.Posts", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Posts", "CreateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Posts", "CreateDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "UpdateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductCategories", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.ProductCategories", "CreateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.ProductCategories", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Products", "UpdateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "UpdateDate", c => c.DateTime());
            AddColumn("dbo.Products", "CreateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Products", "CreateDate", c => c.DateTime());
            AddColumn("dbo.Orders", "CreateBy", c => c.String(maxLength: 50));
            AddColumn("dbo.Orders", "CreateDate", c => c.DateTime());
            DropColumn("dbo.PostCategories", "UpdatedBy");
            DropColumn("dbo.PostCategories", "UpdatedDate");
            DropColumn("dbo.PostCategories", "CreatedBy");
            DropColumn("dbo.PostCategories", "CreatedDate");
            DropColumn("dbo.Posts", "UpdatedBy");
            DropColumn("dbo.Posts", "UpdatedDate");
            DropColumn("dbo.Posts", "CreatedBy");
            DropColumn("dbo.Posts", "CreatedDate");
            DropColumn("dbo.ProductCategories", "UpdatedBy");
            DropColumn("dbo.ProductCategories", "UpdatedDate");
            DropColumn("dbo.ProductCategories", "CreatedBy");
            DropColumn("dbo.ProductCategories", "CreatedDate");
            DropColumn("dbo.Products", "UpdatedBy");
            DropColumn("dbo.Products", "UpdatedDate");
            DropColumn("dbo.Products", "CreatedBy");
            DropColumn("dbo.Products", "CreatedDate");
            DropColumn("dbo.Orders", "CreatedBy");
            DropColumn("dbo.Orders", "CreatedDate");
        }
    }
}