using System;
using System.Globalization;
using P05.AllProblems.Data.Models;

namespace _05.AllProblems
{

    using System;
    using System.Globalization;
    using P05.AllProblems.Data.Models;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;

    public class Program
    {
        static void Main()
        {
            var dbContext = new SoftUniContext();
            //03. EmployeesFullInformation(dbContext);
            //04.EmployeesWithSalaryOver50000(dbContext);
            //05.EmployeesfromResearchandDevelopment(dbContext);
            //06.AddingANewAddressAndUpdatingEmployee(dbContext);
            //07.EmployeesAndProjects(dbContext);
            //08. AddressesByTown(dbContext);
            //09.Employee147(dbContext);
            //10.DepsrtmentsWithMoreThan5Employees(dbContext);
            //11.FindLatest10Projects(dbContext);
            //12.IncreaseSalaries(dbContext);
            //13.EmployeesFirstNameStartingWithSa(dbContext);
            //14.DeleteProjectById(dbContext);
            //15.RemoveTownAndAddressByTownName(dbContext);

        }

        private static void RemoveTownAndAddressByTownName(SoftUniContext dbContext)
        {
            var townToDelete = Console.ReadLine();
            var addressCount = 0;
            var town = dbContext
                .Towns
                .Include(t => t.Addresses)
                .FirstOrDefault(t => t.Name == townToDelete);

            if (town != null)
            {
                addressCount = town.Addresses.Count;
                dbContext
                    .Employees
                    .Where(e => e.AddressId != null && town.Addresses.Any(a => a.AddressId == e.AddressId))
                    .ToList()
                    .ForEach(e => e.AddressId = null);

                dbContext.SaveChanges();

                dbContext
                    .Addresses
                    .RemoveRange(town.Addresses);

                dbContext.Towns.Remove(town);

                dbContext.SaveChanges();
            }
            if (addressCount == 1)
            {
                Console.WriteLine($"{addressCount} address in {townToDelete} was deleted");
            }
            else
            {
                Console.WriteLine($"{addressCount} addresses in {townToDelete} were deleted");
            }
        }

        private static void DeleteProjectById(SoftUniContext dbContext)
        {
            var project = dbContext.Projects.Find(2);
            var empPr = dbContext
                .EmployeesProjects
                .Where(e => e.ProjectId == 2).ToList();

            dbContext
                .EmployeesProjects
                .RemoveRange(empPr);

            dbContext
                .Projects
                .Remove(project);
            dbContext.SaveChanges();

            dbContext
                .Projects
                .Take(10)
                .Select(p => p.Name)
                .ToList()
                .ForEach(Console.WriteLine);
        }

        private static void EmployeesFirstNameStartingWithSa(SoftUniContext dbContext)
        {
            var employees = dbContext.Employees.Where(e => e.FirstName.StartsWith("Sa")).OrderBy(e => e.FirstName)
                            .ThenBy(e => e.LastName);
            foreach (var emp in employees)
            {
                Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle} - (${emp.Salary:f2})");
            }
        }

        private static void IncreaseSalaries(SoftUniContext dbContext)
        {
            var employees = dbContext.Employees
                .Where(e => e.Department.Name == "Engineering" || e.Department.Name == "Tool Design" ||
                            e.Department.Name == "Marketing" || e.Department.Name == "Information Services ")
                .OrderBy(e => e.FirstName).ThenBy(e => e.LastName);

            foreach (var emp in employees)
            {
                emp.Salary *= 1.12m;
                Console.WriteLine($"{emp.FirstName} {emp.LastName} (${emp.Salary:f2})");
            }
        }

        private static void FindLatest10Projects(SoftUniContext dbContext)
        {
            var latestProjects = dbContext.Projects.OrderByDescending(p => p.StartDate).Take(10).ToList();
            foreach (var pr in latestProjects.OrderBy(p => p.Name))
            {
                Console.WriteLine($"{pr.Name}");
                Console.WriteLine($"{pr.Description}");
                Console.WriteLine($"{pr.StartDate}");
            }
        }

        private static void DepsrtmentsWithMoreThan5Employees(SoftUniContext dbContext)
        {
            var departments = dbContext.Departments
                            .Include(d => d.Employees)
                            .Where(d => d.Employees.Count > 5).ToList();
            foreach (var department in departments.OrderBy(d => d.Employees.Count)
                .ThenBy(d => d.Name))
            {
                Console.WriteLine($"{department.Name} - {department.Manager.FirstName} {department.Manager.LastName}");
                foreach (var emp in department.Employees.OrderBy(e => e.FirstName).ThenBy(e => e.LastName))
                {
                    Console.WriteLine($"{emp.FirstName} {emp.LastName} - {emp.JobTitle}");
                }
                Console.WriteLine("----------");
            }
        }

        private static void Employee147(SoftUniContext dbContext)
        {
            var employee147 = dbContext.Employees.Include(e => e.EmployeesProjects).ThenInclude(e => e.Project)
                .FirstOrDefault(e => e.EmployeeId == 147);

            Console.WriteLine($"{employee147.FirstName} {employee147.LastName} - {employee147.JobTitle}");
            foreach (var pr in employee147.EmployeesProjects.OrderBy(a => a.Project.Name))
            {
                Console.WriteLine(pr.Project.Name);
            }
        }

        private static void AddressesByTown(SoftUniContext dbContext)
        {
            var addresess = dbContext.Addresses
                .Include(a => a.Employees)
                .Include(a => a.Town)
                .OrderByDescending(a => a.Employees.Count)
                .ThenBy(a => a.Town.Name)
                .ThenBy(a => a.AddressText)
                .Take(10);
            foreach (var add in addresess)
            {
                Console.WriteLine($"{add.AddressText}, {add.Town.Name} - {add.Employees.Count} employees");
            }
        }

        private static void EmployeesAndProjects(SoftUniContext dbContext)
        {
            var first30EmployeesInPeriod = dbContext
                             .Employees
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
                Console.WriteLine();
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
                    Console.WriteLine($"--{p.Project.Name} - {startDate} - {endDate}");
                }
            }
        }

        private static void AddingANewAddressAndUpdatingEmployee(SoftUniContext dbContext)
        {
            var address = new Address() { AddressText = "Vitoshka 15", TownId = 4 };
            Employee employee = dbContext.Employees.FirstOrDefault(e => e.LastName == "Nakov");
            employee.Address = address;
            dbContext.SaveChanges();
            var employeeAddresses = dbContext.Employees.OrderByDescending(e => e.AddressId).Select(e => new { e.Address.AddressText }).Take(10);
            foreach (var ad in employeeAddresses)
            {
                Console.WriteLine(ad.AddressText);
            }
        }

        private static void EmployeesfromResearchandDevelopment(SoftUniContext dbContext)
        {
            var empFromDep = dbContext
                            .Employees
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
                .Employees
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
            var employees = dbContext.Employees.ToList();
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

