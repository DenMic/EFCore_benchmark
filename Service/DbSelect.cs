using BenchmarkDotNet.Attributes;

using Infrastructure;
using Infrastructure.DTO;
using Infrastructure.Model;

using Microsoft.EntityFrameworkCore;

using Service.Helper;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Service
{
    [MemoryDiagnoser]
    public class DbSelect
    {
        public bool inMemoryDb = false;

        [Params(true, false)]
        public bool EnableTraking { get; set; }

        [Params(true, false)]
        public bool EnableLazyLoading { get; set; }

        [Params(10_000)]
        public int Take { get; set; }


        public DbSelect() { }

        [Benchmark]
        public async Task<List<Supplier>> SelectCompanyAsync()
        {
             using var context = new MyContext(
                 ContextOptionsHelper.GetCorrectOptions(inMemoryDb),
                 EnableTraking, 
                 EnableLazyLoading);

            return await context.GetSet<Supplier>(EnableTraking)
                .Take(Take)
                .ToListAsync();
        }

        [Benchmark]
        public async Task<List<Supplier>> SelectCompanyWithIncludeAsync()
        {
            using var context = new MyContext(
                 ContextOptionsHelper.GetCorrectOptions(inMemoryDb),
                 EnableTraking,
                 EnableLazyLoading);

            var qCompany = context.GetSet<Supplier>(EnableTraking)
                    .Include(x => x.Products)
                    .Take(Take);

            if (EnableTraking)
                qCompany = qCompany.AsNoTracking();

            return await qCompany
                .ToListAsync();
        }
    }
}
