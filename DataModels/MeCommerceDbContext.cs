using System.Data.Entity;

namespace DataModels
{
    public class MeCommerceDbContext : DbContext
    {
        public MeCommerceDbContext()
            : base("name=MeCommerceDbContext")
        {
        }

        public virtual DbSet<Brand> Brands { get; set; }

        public virtual DbSet<Category> Categories { get; set; }

        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }

        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }

        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }

        public virtual DbSet<Order> Orders { get; set; }

        public virtual DbSet<OrderLine> OrderLines { get; set; }

        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }

        public virtual DbSet<Product> Products { get; set; }

        public virtual DbSet<ShoppingCart> ShoppingCart { get; set; }

        public virtual DbSet<ShoppingCartItem> ShoppingCartItem { get; set; }

        public virtual DbSet<BrowsingHistory> BrowsingHistory { get; set; }

        public virtual DbSet<Device> Devices { get; set; }
    }
}