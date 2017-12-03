using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using Employees.DtoModels;
using Employees.Models;

namespace Employees.App
{
    public class AutomapperProfile:Profile
    {
        public AutomapperProfile()
        {
            this.CreateMap<Employee, EmployeeDto>();
            this.CreateMap<EmployeeDto, Employee>();
            this.CreateMap<Employee, EmployeePersonalDto>();
            this.CreateMap<Employee, ManagerDto>()
                .ForMember(d=>d.Employees,o=>o.MapFrom(src=>src.Employees.Count))
                .ForMember(d=>d.Employees,o=>o.MapFrom(src=>src.Employees));
            this.CreateMap<ManagerDto, Employee>();
            this.CreateMap<Employee, EmployeeWithManagerDto>()
                .ForMember(d => d.ManagerName, o => o.MapFrom(src => src.Manager))
                .ForMember(d => d.ManagerName, o => o.MapFrom(src => src.LastName)); ;
            this.CreateMap<EmployeeWithManagerDto, Employee>();

        }
    }
}
