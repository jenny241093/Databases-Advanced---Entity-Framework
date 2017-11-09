using System;
using System.Linq;


public class Program
    {
        static void Main()
        {
           var context=new SoftUniDbContext();
            var employees = context.Employees.ToList();
            Console.WriteLine();
        }
    }
//DESKTOP-5CTIM8C\SQLEXPRESS
