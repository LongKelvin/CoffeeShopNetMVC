using CoffeeShop.Models.Models;

using Microsoft.AspNet.Identity.EntityFramework;

using System.Data.Entity;

namespace CoffeeShop.Data
{
    public class CoffeeShopDbContext : IdentityDbContext<ApplicationUser>
    {
        public CoffeeShopDbContext() : base("CoffeeShopConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<CoffeeShopDbContext, CoffeeShop.Data.Migrations.Configuration>());
            this.Configuration.LazyLoadingEnabled = false;
            this.Configuration.ProxyCreationEnabled = false;
          
        }

        public virtual DbSet<Footer> Footers { get; set; }
        public virtual DbSet<MenuGroup> MenuGroups { get; set; }
        public virtual DbSet<Menu> Menus { get; set; }
        public virtual DbSet<OrderDetail> OrderDetails { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<Page> Pages { get; set; }
        public virtual DbSet<PostCategory> PostCategories { get; set; }
        public virtual DbSet<Post> Posts { get; set; }
        public virtual DbSet<ProductCategory> ProductCategories { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<SupportOnline> SupportOnlines { get; set; }
        public virtual DbSet<SystemConfig> SystemConfigs { get; set; }
        public virtual DbSet<Tag> Tags { get; set; }
        public virtual DbSet<VisitorStatistic> VisitorStatistics { get; set; }
        public virtual DbSet<Slide> Slides { get; set; }
        public virtual DbSet<Error> Errors { get; set; }
        public virtual DbSet<ShopInformation> ShopInfos { get; set; }
        public virtual DbSet<ShopPaymentInfo> ShopPayments { get; set; }
        public virtual DbSet<Feedback> Feedbacks { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }

        public virtual DbSet<ApplicationGroup> ApplicationGroups { get; set; }
        public virtual DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public virtual DbSet<ApplicationRoleGroup> ApplicationRoleGroups { get; set; }
        public virtual DbSet<ApplicationUserGroup> ApplicationUserGroups { get; set; }

        public static CoffeeShopDbContext Create()
        {
            return new CoffeeShopDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Footer>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Menu>()
                .Property(e => e.Target)
                .IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Order)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<PostCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<PostCategory>()
                .HasMany(e => e.Posts)
                .WithOptional(e => e.PostCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Post>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Post>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Posts)
                .Map(m => m.ToTable("PostTags")
                .MapLeftKey("PostID").MapRightKey("TagID"));

            modelBuilder.Entity<ProductCategory>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<ProductCategory>()
                .HasMany(e => e.Products)
                .WithOptional(e => e.ProductCategory)
                .HasForeignKey(e => e.CategoryID);

            modelBuilder.Entity<Product>()
                .Property(e => e.Alias)
                .IsUnicode(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.OrderDetails)
                .WithRequired(e => e.Product)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Product>()
                .HasMany(e => e.Tags)
                .WithMany(e => e.Products)
                .Map(m => m.ToTable("ProductTags")
                .MapLeftKey("ProductID").MapRightKey("TagID"));

            modelBuilder.Entity<Tag>()
                .Property(e => e.ID)
                .IsUnicode(false);

            modelBuilder.Entity<Tag>()
                .Property(e => e.Type)
                .IsUnicode(false);

            modelBuilder.Entity<VisitorStatistic>()
                .Property(e => e.IPAddress)
                .IsUnicode(false);

            //Identity
            modelBuilder.Entity<IdentityUserRole>().ToTable("ApplicationUserRoles")
                .Property(p => p.UserId).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserRole>().Property(p => p.RoleId)
                .HasColumnName("RoleId");

            modelBuilder.Entity<IdentityUserRole>().HasKey(k => new { k.UserId, k.RoleId });

            modelBuilder.Entity<IdentityUserLogin>().ToTable("ApplicationUserLogins")
                .Property(p => p.UserId).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserLogin>().HasKey(k => new { k.UserId });

            modelBuilder.Entity<IdentityRole>().ToTable("ApplicationRoles");
            modelBuilder.Entity<IdentityRole>().HasKey(k => new { k.Id });

            modelBuilder.Entity<IdentityUserClaim>().ToTable("ApplicationUserClaims")
                 .Property(p => p.UserId).HasColumnName("UserId");
            modelBuilder.Entity<IdentityUserClaim>().HasKey(k => new { k.UserId });
        }
    }
}