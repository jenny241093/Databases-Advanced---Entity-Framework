using System;
using System.Collections.Generic;
using System.Text;
using Employees.Services;

namespace Employees.App.Commands
{
    public class SetBirthdayCommand:ICommand
    {
        private readonly EmployeeService employeeService;

        public SetBirthdayCommand(EmployeeService employeeService)
        {
            this.employeeService = employeeService;
        }
        public string Execute(params string[] args)
        {
            var employeeId = int.Parse(args[0]);
            DateTime date = DateTime.ParseExact(args[1], "dd-MM-yyyy", null);
           var employeeName= this.employeeService.SetBirthday(employeeId,date);
            return $"{employeeName}'s birthday was set to {args[1]}";
        }
    }
}
