
using Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace EF_performance.ExtensionMethod
{
    public static class EFContext
    {
        public static void AddEFContext(this IServiceCollection services, bool inMemoryDb)
        {
            services.AddDbContext<MyContext>(options =>
            {
                if (inMemoryDb)
                {
                    options.UseInMemoryDatabase(databaseName: $"Select");
                }
                else
                {
                    options.UseSqlServer("Data Source=.\\;Database=BenchmarkEF;Integrated Security=SSPI;MultipleActiveResultSets=true")
                        .EnableSensitiveDataLogging();
                }
            });
        }

        //private static void CreateInMemoryDb()
        //{
        //   PrepareDb(context);
        //}

        //private static void PrepareDb(MyContext context)
        //{
        //    var rnd = new Random();

        //    for (var i = 0; i < 100_000; i++)
        //    {
        //        var company = new Company
        //        {
        //            BusinessName = "Test",
        //            Piva = "12345678912",
        //        };

        //        var rndNumber = rnd.Next(0, 10);
        //        var employees = new List<Employee>();
        //        for (var j = 0; j < rndNumber; j++)
        //        {
        //            employees.Add(new Employee()
        //            {
        //                FirstName = "FirstName",
        //                LastName = "LastName",
        //            });
        //        }

        //        company.Employees = employees;
        //        context.Companies.Add(company);
        //    }

        //    context.SaveChanges();
        //}
    }
}
