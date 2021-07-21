using BenchmarkDotNet.Running;
using EF_performance.Business;
using EF_performance.EF;
using EF_performance.EF.Model;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EF_performance
{
    class Program
    {
        private static DbContextOptions<MyContext> options = new DbContextOptionsBuilder<MyContext>()
                .UseInMemoryDatabase(databaseName: $"Select")
                .Options;

        static async Task Main(string[] args)
        {
            // Insert Test
            // BenchmarkRunner.Run<DbInsert>();        

            // Select Test
            using (var context = new MyContext(options))
            {
                PrepareDb(context);
            }

            BenchmarkRunner.Run<DbSelect>();
        }

        private static void PrepareDb(MyContext context)
        {
            var rnd = new Random();

            for (var i = 0; i < 10_000; i++)
            {
                var company = new Company
                {
                    BusinessName = "Test",
                    Piva = "12345678912",
                };

                var rndNumber = rnd.Next(0, 10);
                var employees = new List<Employee>();
                for (var j = 0; j < rndNumber; j++)
                {
                    employees.Add(new Employee()
                    {
                        FirstName = "FirstName",
                        LastName = "LastName",
                    });
                }

                company.Employees = employees;
                context.Companies.Add(company);
            }

            context.SaveChanges();
        }
    }
}
