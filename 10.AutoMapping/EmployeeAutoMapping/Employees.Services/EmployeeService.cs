using System;
using System.Linq;
using AutoMapper;
using Employees.Data;
using Employees.DtoModels;
using Employees.Models;
using AutoMapper.QueryableExtensions;
namespace Employees.Services
{
    public class EmployeeService
    {
        private readonly EmployeesContext context;

        public EmployeeService(EmployeesContext context)
        {
            this.context = context;
        }

        public EmployeeDto ById(int employeeId)
        {
            var employee =context.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public void AddEmployee(EmployeeDto dto)
        {
            var employee = Mapper.Map<Employee>(dto);
            context.Employees.Add(employee);
            context.SaveChanges();
        }

        public string SetBirthday(int employeeId, DateTime date)
        {
            var employee = this.context.Employees.Find(employeeId);
            employee.Birthday = date;
            context.SaveChanges();
            return $"{employee.FirstName} {employee.LastName}";
        }
        public string SetAddress(int employeeId, string address)
        {
            var employee = this.context.Employees.Find(employeeId);
            employee.Address = address;
            context.SaveChanges();
            return $"{employee.FirstName} {employee.LastName}";
        }

        public EmployeePersonalDto EmployeePersonalInfo(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            var employeeDto = Mapper.Map<EmployeePersonalDto>(employee);
            return employeeDto;
        }

        public string[] SetManager(int employeeId, int managerId)
        {
            var employee = this.context.Employees.Find(employeeId);
            var manager = this.context.Employees.Find(managerId);
            employee.Manager = manager;
            manager.Employees.Add(employee);
            context.SaveChanges();
            var names = new string[]
            {
                $"{employee.FirstName} {employee.LastName}",
                $"{manager.FirstName} {manager.LastName}"
            };
            return names;
        }

        public bool DoesEmployeeExistById(int id)
        {
            return this.context.Employees.Any(e => e.Id == id);
        }
        public bool DoesEmployeeExistByName(string firstName,string lastName)
        {
            return context.Employees.Any(e => e.FirstName == firstName&&e.LastName==lastName);
        }
        public ManagerDto GetManagerInfoById(int employeeId)
        {
            var employee = context.Employees.Find(employeeId);
            var managerDto = Mapper.Map<ManagerDto>(employee);
            return managerDto;
        }

        public EmployeeWithManagerDto[] GetEmployeesWithManagersByAge(int age)
        {
            var employees = this.context.Employees.Where(e => (DateTime.Now.Year - e.Birthday.Value.Year) > age)
                .ProjectTo<EmployeeWithManagerDto>()
                .ToArray();
            return employees;
        }
    }
}
