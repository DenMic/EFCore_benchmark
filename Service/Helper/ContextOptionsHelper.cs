using Infrastructure;

using Microsoft.EntityFrameworkCore;

namespace Service.Helper
{
    internal static class ContextOptionsHelper
    {
        private static readonly DbContextOptions<MyContext> ctxOptionsSql = new DbContextOptionsBuilder<MyContext>()
            .UseSqlServer("Data Source=.\\;Database=BenchmarkEF;Integrated Security=SSPI;MultipleActiveResultSets=true")
            .EnableSensitiveDataLogging()
            .Options;

        private static readonly DbContextOptions<MyContext> ctxOptionInMemory = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"Insert")
                .Options;

        internal static DbContextOptions<MyContext> GetCorrectOptions(bool inMemory)
        {
            return inMemory ? ctxOptionInMemory : ctxOptionsSql;
        }
    }
}
