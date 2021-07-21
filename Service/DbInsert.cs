using BenchmarkDotNet.Attributes;

using Infrastructure;
using Infrastructure.Model;

using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Service
{
    [MemoryDiagnoser]
    public class DbInsert
    {
        [Params(100_000)]
        public int insertNumber { get; set; }

        [Benchmark]
        public async Task InsertCompanyAsync()
        {
            var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"Insert")
                .Options;

            using var context = new MyContext(options);

            //for (var i = 0; i < insertNumber; i++)
            //{
            //    await context.Companies.AddAsync(new Company
            //    {
            //        BusinessName = "Test",
            //        Piva = "12345678912"
            //    });
            //}

            await context.SaveChangesAsync();
        }
        
        [Benchmark]
        public void InsertCompany()
        {
            var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"InsertNoAsync")
                .Options;

            using var context = new MyContext(options);

            var taskList = new List<ValueTask>();
            for (var i = 0; i < insertNumber; i++)
            {
                //context.Companies.Add(new Company
                //{
                //    BusinessName = "Test",
                //    Piva = "12345678912"
                //});
            }

            context.SaveChanges();
        }

        [Benchmark]
        public async Task InsertCompanySaveTenThousandAsync()
        {
            var options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"InsertTenThousand")
                .Options;

            using var context = new MyContext(options);

            int i;
            for (i = 0; i < insertNumber; i++)
            {
                //await context.Companies.AddAsync(new Company
                //{
                //    BusinessName = "Test",
                //    Piva = "12345678912"
                //});

                if (i % 10000 == 0)
                    await context.SaveChangesAsync();
            }

            if (i % 10000 != 0)
                await context.SaveChangesAsync();
        }
    }
}
