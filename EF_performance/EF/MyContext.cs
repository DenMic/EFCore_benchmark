using EF_performance.EF.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_performance.EF
{
    public class MyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public IQueryable<T> GetSet<T>(bool traking = false) where T: class
        {
            return traking ? Set<T>() : Set<T>().AsNoTracking();
        }

        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

        public MyContext(DbContextOptions<MyContext> options, bool noTraking, bool lazyLoading)
        : base(options)
        {
            ChangeTracker.QueryTrackingBehavior = noTraking ? QueryTrackingBehavior.NoTracking : QueryTrackingBehavior.TrackAll;
            ChangeTracker.LazyLoadingEnabled = lazyLoading;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Company>()
                .HasMany(x => x.Employees)
                .WithOne(x => x.Company);
        }
    }
}
