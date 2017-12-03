using System;
using System.Collections.Generic;
using System.Text;
using Employees.DtoModels;
using Employees.Services;

namespace Employees.App.Commands
{
   public  class AddEmployeeCommand:ICommand
   {
       private readonly EmployeeService employeeService;

       public AddEmployeeCommand(EmployeeService employeeService)
       {
           this.employeeService = employeeService;
       }
        public string Execute(params string[] args)
        {
            var firstName = args[0];
            var lastName = args[1];
            var salary = decimal.Parse(args[2]);
            var employeeDto = new EmployeeDto(firstName,lastName,salary);

            this.employeeService.AddEmployee(employeeDto);
            return $"{firstName} {lastName} successfully added.";
        }
    }
}
