using System;
using System.Collections.Generic;
using System.Text;

namespace Employees.DtoModels
{
   public class ManagerDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public decimal Salary { get; set; }
        public int EmployeesCount { get; set; }
        
        public ICollection<EmployeeDto> Employees { get; set; }=new List<EmployeeDto>();

    }
}
