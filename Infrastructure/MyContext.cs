using Infrastructure.Model;

using Microsoft.EntityFrameworkCore;

using System.Linq;

namespace Infrastructure
{
    public class MyContext : DbContext
    {
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

        public IQueryable<T> GetSet<T>(bool traking = false) where T : class
        {
            return traking ? Set<T>() : Set<T>().AsNoTracking();
        }

        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

        public MyContext(DbContextOptions<MyContext> options, bool enableTraking, bool lazyLoading)
        : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = enableTraking ? QueryTrackingBehavior.TrackAll : QueryTrackingBehavior.NoTracking;
            ChangeTracker.LazyLoadingEnabled = lazyLoading;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>()
                .HasMany(x => x.Orders)
                .WithOne(x => x.Customer)
                .HasForeignKey(x => x.CustomerId);

            modelBuilder.Entity<Supplier>()
                .HasMany(x => x.Products)
                .WithOne(x => x.Supplier)
                .HasForeignKey(x => x.SupplierId);

            modelBuilder.Entity<Order>()
                .HasMany(p => p.Products)
                .WithMany(p => p.Orders)
                .UsingEntity<OrderItem>(
                    x => x.HasOne(y => y.Product)
                        .WithMany(y => y.OrderItems)
                        .HasForeignKey(y => y.ProductId),
                    x => x.HasOne(y => y.Order)
                        .WithMany(y => y.OrderItems)
                        .HasForeignKey(y => y.OrderId)
                 );
        }
    }
}
