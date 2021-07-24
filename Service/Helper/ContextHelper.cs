using Infrastructure;

using Microsoft.EntityFrameworkCore;

using System;
using System.IO;
using System.Linq;

namespace Service.Helper
{
    internal static class ContextHelper
    {
        private static string DbPath { get; set; }

        private static readonly DbContextOptions<MyContext> ctxOptionsSql = new DbContextOptionsBuilder<MyContext>()
            .UseSqlServer("Data Source=.\\;Database=BenchmarkEF;Integrated Security=SSPI;MultipleActiveResultSets=true")
            .EnableSensitiveDataLogging()
            //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
            .Options;

        private static DbContextOptions<MyContext> ctxOptionSqliteInMemory { get {
                return new DbContextOptionsBuilder<MyContext>()
                .UseSqlite(@$"DataSource={DbPath}")
                .EnableSensitiveDataLogging()
                //.LogTo(Console.WriteLine, Microsoft.Extensions.Logging.LogLevel.Information)
                .Options;
            }
        }

        internal static DbContextOptions<MyContext> GetCorrectOptions(bool isSqliteDb, bool createDb = false)
        {
            if (isSqliteDb && createDb)
            {
                var folder = Environment.SpecialFolder.LocalApplicationData;
                var path = Environment.GetFolderPath(folder);
                DbPath = $"{path}{Path.DirectorySeparatorChar}inMemory.db";

                if (File.Exists(DbPath))
                    File.Delete(DbPath);
            }

            return isSqliteDb ? ctxOptionSqliteInMemory : ctxOptionsSql;
        }

        internal static void PrepareInMemoryDb()
        {
            using var context = new MyContext(GetCorrectOptions(true, true), createDb: true);

            var assembly = AppDomain.CurrentDomain.GetAssemblies().Where(x => x.FullName.Contains("Infrastructure")).FirstOrDefault();
            var basePath = Path.GetDirectoryName(assembly.Location);

            var pathInsert = Path.Combine(basePath, @"DbScript\sample-data-inmemory.sql");
            var sql = File.ReadAllText(pathInsert);
            if (!string.IsNullOrWhiteSpace(sql))
                context.Database.ExecuteSqlRaw(sql);
        }
    }
}
