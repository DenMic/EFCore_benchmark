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
        // Database inMemory not work
        public bool inMemoryDb = true;

        [Params(true, false)]
        public bool EnableTraking { get; set; }

        [Params(true, false)]
        public bool EnableLazyLoading { get; set; }

        [Params(true, false)]
        public bool SplitQuery { get; set; }

        [Params(100)]
        public int Take { get; set; }

        public DbSelect() {
            if (inMemoryDb)
                ContextHelper.PrepareInMemoryDb();
        }

        [Benchmark]
        public async Task<List<SupplierDto>> SelectCompanyAsync()
        {
            using var context = new MyContext(
                ContextHelper.GetCorrectOptions(inMemoryDb),
                EnableTraking,
                EnableLazyLoading);

            var lSupplier = await context.GetSet<Supplier>(EnableTraking)
                .Take(Take)
                .ToListAsync();

            return lSupplier.Select(x => new SupplierDto()
                {
                    Name = x.CompanyName
                })
                .ToList();
        }

        [Benchmark]
        public async Task<List<SupplierDto>> SelectCompanyWithIncludeAsync()
        {
            using var context = new MyContext(
                 ContextHelper.GetCorrectOptions(inMemoryDb),
                 EnableTraking,
                 EnableLazyLoading);

            var qSupplier = context.GetSet<Supplier>(EnableTraking)
                    .Include(x => x.Products)
                    .Take(Take);

            if (EnableTraking)
                qSupplier = qSupplier.AsNoTracking();

            if (SplitQuery)
                qSupplier = qSupplier.AsSplitQuery();

            var lSupplier = await qSupplier.ToListAsync();

            // Select performed here to test lazy loading
            return lSupplier.Select(x => new SupplierDto()
                {
                   Name = x.CompanyName,
                   Product = x.Products.Select(y => new ProductDto()
                   {
                       Name = y.ProductName
                   })
                })
                .ToList();
        }
    }
}
