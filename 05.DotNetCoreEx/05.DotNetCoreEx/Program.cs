using System;
using P02_DatabaseFirst.Data.Models;
using P02_DatabaseFirst.Data;
using System.Linq;

namespace P02_DatabaseFirst
{
  public  class Program
    {
        static void Main(string[] args)
        {
            var dbContext = new SoftUniContext();
            var employees = dbContext.Employees.ToList();
            foreach (var emp in employees)
            {
                Console.Write($"{emp.FirstName} ");
                Console.Write($"{emp.LastName} ");
                Console.Write($"{emp.MiddleName} ");
                Console.Write($"{emp.JobTitle} ");
                Console.Write($"{emp.Salary:F2}");
                Console.WriteLine();

            }
        }
    }
}
