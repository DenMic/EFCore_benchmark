using BenchmarkDotNet.Running;

using Service;

using System;
using System.Threading.Tasks;

namespace EF_performance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            //var serviceProvider = new ServiceCollection();
            //serviceProvider.AddEFContext(InMemoryDb);

            // Insert Test
            // BenchmarkRunner.Run<DbInsert>();

            // Select Test
            // BenchmarkRunner.Run<DbSelect>();

            var a = await new DbSelect().SelectCompanyAsync();
            Console.WriteLine(a.Count);
        }


    }
}
