
namespace P02_DatabaseFirst
{
    using System;
    using P02_DatabaseFirst.Data;
    using System.Linq;
    using P02_DatabaseFirst.Data.Models;
    using System.Globalization;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        static void Main()
        {
            var dbContext = new SoftUniContext();
            //03. EmployeesFullInformation(dbContext);
            //04.EmployeesWithSalaryOver50000(dbContext);
            EmployeesfromResearchandDevelopment(dbContext);
            //06.AddingANewAddressAndUpdatingEmployee(dbContext);
            //07.EmployeesAndProjects(dbContext);
        }

        private static void EmployeesAndProjects(SoftUniContext dbContext)
        {
            var first30EmployeesInPeriod = dbContext
                             .Employee
                             .Include(e => e.EmployeesProjects)
                             .ThenInclude(ep => ep.Project)
                             .Where(e => e.EmployeesProjects.Any(p => p.Project.StartDate.Year >= 2001 && p.Project.StartDate.Year <= 2003))
                             .Take(30)
                             .Select(e => new
                             {
                                 e.FirstName,
                                 e.LastName,
                                 e.Manager,
                                 e.EmployeesProjects
                             })
                             .ToList();
            foreach (var em in first30EmployeesInPeriod)
            {
                Console.Write($"{em.FirstName} {em.LastName} - Manager: {em.Manager.FirstName} {em.Manager.LastName}");
                foreach (var p in em.EmployeesProjects)
                {
                    string format = "M/d/yyyy h:mm:ss tt";

                    string startDate = p.Project.StartDate.ToString(format, CultureInfo.InvariantCulture);
                    string endDate = p.Project.EndDate.ToString();

                    if (string.IsNullOrWhiteSpace(endDate))
                    {
                        endDate = "not finished";
                    }
                    else
                    {
                        endDate = p.Project.EndDate.Value.ToString(format, CultureInfo.InvariantCulture);
                    }
                    Console.WriteLine($"{p.Project.Name} - {startDate} {endDate}");
                }
            }
        }

        private static void AddingANewAddressAndUpdatingEmployee(SoftUniContext dbContext)
        {
            var address = new Address() { AddressText = "Vitoshka 15", TownId = 4 };
            Employee employee = dbContext.Employee.FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = address;
            dbContext.SaveChanges();
            var employeeAddresses = dbContext.Employee.OrderByDescending(e => e.AddressId).Select(e => new { e.Address.AddressText }).Take(10);
            foreach (var ad in employeeAddresses)
            {
                Console.WriteLine(ad.AddressText);
            }
        }

        private static void EmployeesfromResearchandDevelopment(SoftUniContext dbContext)
        {
            var empFromDep = dbContext
                            .Employee
                            .Where(e => e.Department.Name == "Research and Development")
                            .OrderBy(e => e.Salary)
                            .ThenByDescending(e => e.FirstName)
                            .Select(e => new
                            {
                                e.FirstName,
                                e.LastName,
                                DepartmentName = e.Department.Name,
                                e.Salary
                            });
            foreach (var emp in empFromDep)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} from {emp.DepartmentName} - ${emp.Salary:f2}");
            }
        }

        private static void EmployeesWithSalaryOver50000(SoftUniContext dbContext)
        {
            var employeesNames = dbContext
                .Employee
                .Where(e => e.Salary > 50000)
                .Select(e => e.FirstName)
                .OrderBy(e => e);
            foreach (var em in employeesNames)
            {
                Console.WriteLine(em);
            }
        }

        private static void EmployeesFullInformation(SoftUniContext dbContext)
        {
            var employees = dbContext.Employee.ToList();
            foreach (var emp in employees.OrderBy(e => e.EmployeeId))
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
