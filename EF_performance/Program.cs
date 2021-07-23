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
            // Insert Test
            // BenchmarkRunner.Run<DbInsert>();

            // Select Test
            // BenchmarkRunner.Run<DbSelect>();

            var dbSelect = new DbSelect();
            var companies = await dbSelect.SelectCompanyAsync();
            var companiesInclude = await dbSelect.SelectCompanyWithIncludeAsync();

            Console.WriteLine("End");
        }


    }
}
