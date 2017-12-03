using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Commands
{
    public class SetManagerCommand:ICommand
    {
        private readonly EmployeeService employeeService;

        public SetManagerCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            var managerId = int.Parse(args[1]);
            if (!this.employeeService.DoesEmployeeExistById(employeeId))
            {
                throw new ArgumentException($"Employee with {employeeId} id does not exist.");
            }
            if (!employeeService.DoesEmployeeExistById(managerId))
            {
                throw new ArgumentException($"Manager with {employeeId} id does not exist.");
            }
            var names = employeeService.SetManager(employeeId, managerId);

            var employeeName = names[0];
            var managerName = names[1];

            return $"Successfully set {managerName} for manager of {employeeName}";
        }
    }
}
