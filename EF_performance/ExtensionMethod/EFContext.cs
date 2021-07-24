
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
    }
}
