using BenchmarkDotNet.Attributes;
using EF_performance.EF;
using EF_performance.EF.Model;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF_performance.Business
{
    [MemoryDiagnoser]
    public class DbSelect
    {
        [Params(true, false)]
        public bool enableTraking { get; set; }

        private DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"Select")
                .Options;

        public DbSelect()
        {
            using var context = new MyContext(options);
            PrepareDb(context);
        }

        [Benchmark]
        public async Task<List<Company>> SelectCompanyAsync()
        {
            using var context = new MyContext(options);

            return await context.GetSet<Company>(enableTraking).ToListAsync();
        }

        private void PrepareDb(MyContext context)
        {
            for (var i = 0; i < 10_000; i++)
            {
                context.Companies.Add(new Company
                {
                    BusinessName = "Test",
                    Piva = "12345678912"
                });
            }

            context.SaveChanges();
        }
    }
}
