using BenchmarkDotNet.Running;
using EF_performance.Business;
using System.Threading.Tasks;

namespace EF_performance
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // Insert Test
            BenchmarkRunner.Run<DbInsert>();        

            // Select Test
            // BenchmarkRunner.Run<DbSelect>();
        }
    }
}
