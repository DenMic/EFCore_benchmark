using EF_performance.EF.Model;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace EF_performance.EF
{
    public class MyContext : DbContext
    {
        public DbSet<Company> Companies { get; set; }

        public IQueryable<T> GetSet<T>(bool traking = false) where T: class
        {
            return traking ? Set<T>() : Set<T>().AsNoTracking();
        }

        public MyContext(DbContextOptions<MyContext> options)
        : base(options)
        { }

    }
}
