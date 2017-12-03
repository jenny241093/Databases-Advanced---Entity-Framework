using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Commands
{
   public class EmployeeInfoCommand:ICommand
    {
        private readonly EmployeeService employeeService;

        public EmployeeInfoCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
            //employeeId
            int employeeId = int.Parse(args[0]);
            var employee = this.employeeService.ById(employeeId);
            return $"ID: {employeeId} - {employee.FirstName} {employee.LastName} -  ${employee.Salary:f2}";
        }
    }
}
