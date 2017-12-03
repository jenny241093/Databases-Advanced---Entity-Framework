using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Employees.Services;

namespace Employees.App.Commands
{
  public   class ListEmployeesOlderThanCommand:ICommand
    {

        private readonly EmployeeService employeeService;

        public ListEmployeesOlderThanCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }

        public string Execute(params string[] args)
        {
            int age = int.Parse(args[0]);
            var employees = this.employeeService.GetEmployeesWithManagersByAge(age)
                .OrderByDescending(e => e.Salary);
            var result=new StringBuilder();
            
            foreach (var e in employees)
            {
                var managerName = "[no manager]";
                if (e.ManagerName!=null)
                {
                    managerName = e.ManagerName;
                }
                result.AppendLine($"{e.FirstName} {e.LastName} - ${e.Salary:F2} - Manager: {managerName}");
            }
            return result.ToString().Trim();


        }
    }
}
