using Infrastructure;

using Microsoft.EntityFrameworkCore;

using System;
using System.IO;
using System.Linq;

namespace Service.Helper
{
    internal static class ContextHelper
    {
        private static readonly DbContextOptions<MyContext> ctxOptionsSql = new DbContextOptionsBuilder<MyContext>()
            .UseSqlServer("Data Source=.\\;Database=BenchmarkEF;Integrated Security=SSPI;MultipleActiveResultSets=true")
            .EnableSensitiveDataLogging()
            .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            .Options;

        //private static readonly DbContextOptions<MyContext> ctxOptionInMemory = new DbContextOptionsBuilder<MyContext>()
        //        .UseInMemoryDatabase(databaseName: $"Select")
        //        .EnableSensitiveDataLogging()
        //        .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
        //        .Options;

        private static readonly DbContextOptions<MyContext> ctxOptionSqliteInMemory = new DbContextOptionsBuilder<MyContext>()
                .UseSqlite("DataSource=:memory:")
                .EnableSensitiveDataLogging()
                .LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .Options;

        internal static DbContextOptions<MyContext> GetCorrectOptions(bool inMemory)
        {
            return inMemory ? ctxOptionSqliteInMemory : ctxOptionsSql;
        }

        internal static void PrepareInMemoryDb()
        {
            using var context = new MyContext(ctxOptionSqliteInMemory);

            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Infrastructure")).FirstOrDefault();
            var basePath = Path.GetDirectoryName(assembly.Location);

            var pathModel = Path.Combine(basePath, @"DbScript\sample-model-inmemory.sql");
            var sql = File.ReadAllText(pathModel);
            if (!string.IsNullOrWhiteSpace(sql))
                context.Database.ExecuteSqlRaw(sql);

            var pathInsert = Path.Combine(basePath, @"DbScript\1_InsertCustomer.sql");
            sql = File.ReadAllText(pathInsert);
            if (!string.IsNullOrWhiteSpace(sql))
                context.Database.ExecuteSqlRaw(sql);
        }
    }
}
