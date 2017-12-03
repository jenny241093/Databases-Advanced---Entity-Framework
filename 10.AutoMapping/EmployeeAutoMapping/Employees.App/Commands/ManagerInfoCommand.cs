using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Employees.Services;

namespace Employees.App.Commands
{
   public class ManagerInfoCommand:ICommand
    {
        private readonly EmployeeService employeeService;

        public ManagerInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
          
            var employeeId = int.Parse(args[0]);
            if (!this.employeeService.DoesEmployeeExistById(employeeId))
            {
                throw new ArgumentException($"Employee with {employeeId} id does not exist.");
            }
            var manager = this.employeeService.GetManagerInfoById(employeeId);
            StringBuilder output=new StringBuilder();
            output.AppendLine($"{manager.FirstName} {manager.LastName} | Employees: {manager.EmployeesCount}");
            var employees = manager.Employees.ToList();
           employees.ForEach(e=>output.AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:f2}"));
            return output.ToString().Trim();
        }
    }
}
