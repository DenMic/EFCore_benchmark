using BenchmarkDotNet.Attributes;

using EF_performance.DTO;
using EF_performance.EF;
using EF_performance.EF.Model;

using Microsoft.EntityFrameworkCore;

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EF_performance.Business
{
    [MemoryDiagnoser]
    public class DbSelect
    {
        [Params(true, false)]
        public bool EnableTraking { get; set; }

        [Params(true, false)]
        public bool EnableLazyLoading { get; set; }

        [Params(2000)]
        public int Take { get; set; }

        private DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"Select")
                .Options;

        [Benchmark]
        public async Task<List<CompanyDto>> SelectCompanyAsync()
        {
            using var context = new MyContext(options, EnableTraking, EnableLazyLoading);

            return await context.GetSet<Company>(EnableTraking)
                .Take(Take)
                .Select(x => new CompanyDto
                {
                    CompanyName = x.BusinessName,
                    Employees = x.Employees
                        .Select(y => new EmployeeDto
                            {
                                Name = $"{y.LastName} {y.FirstName}",
                            }
                        )
                })
                .ToListAsync();
        }

        [Benchmark]
        public async Task<List<CompanyDto>> SelectCompanyWithIncludeAsync()
        {
            using var context = new MyContext(options, EnableTraking, EnableLazyLoading);

            var qCompany = context.GetSet<Company>(EnableTraking)
                    .Include(x => x.Employees)
                    .Take(Take);

            if (EnableTraking)
                qCompany = qCompany.AsNoTracking();

            return await qCompany
                .Select(x => new CompanyDto
                {
                    CompanyName = x.BusinessName,
                    Employees = x.Employees
                        .Select(y => new EmployeeDto
                        {
                            Name = $"{y.LastName} {y.FirstName}",
                        }
                    )
                })
                .ToListAsync();
        }
    }
}
