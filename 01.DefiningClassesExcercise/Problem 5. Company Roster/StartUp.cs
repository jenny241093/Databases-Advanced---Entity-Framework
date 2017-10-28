using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartUp
{
    static void Main(string[] args)
    {
        var employees = new List<Employee>();
        int n = int.Parse(Console.ReadLine());
        for (int i = 0; i < n; i++)
        {
            var cmdArgs = Console.ReadLine().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            var name = cmdArgs[0];
            var salary = double.Parse(cmdArgs[1]);
            var position = cmdArgs[2];
            var department = cmdArgs[3];
            if (cmdArgs.Length == 6)
            {
                var email = cmdArgs[4];
                var age = int.Parse(cmdArgs[5]);
                var employee = new Employee(name, salary, position, department, email, age);
                employees.Add(employee);
            }
            else if (cmdArgs.Length == 5)
            {

                var isAge = 0;
                if (int.TryParse(cmdArgs[4], out isAge))
                {
                    Employee employee = new Employee(name, salary, position, department, isAge);
                }
                else
                {
                    var email = cmdArgs[4];
                    Employee employee = new Employee(name, salary, position, department, email);
                    employees.Add(employee);
                }
            }
            else if (cmdArgs.Length==4)
            {
                var employee = new Employee(name, salary, position, department);
                employees.Add(employee);
            }
        }
        Console.WriteLine(Print(employees));
    }
    public static string Print(List<Employee> employees)
    {
        StringBuilder sb = new StringBuilder();
        var highestSalaryDepartment = employees.Max(x => x.Salary);
        var department = employees.FirstOrDefault(x => x.Salary >= highestSalaryDepartment).Department;
        sb.AppendLine($"Highest Average Salary: {department}");
        var departmentEmployees = employees.Where(x => x.Department == department).ToList();
        foreach (var emp in departmentEmployees.OrderByDescending(x => x.Salary))
        {
            sb.AppendLine(emp.ToString());
        }
        return sb.ToString().Trim();
    }
}

