using AutoMapper;
using CompanyApp.Domain.Entities;
using CompanyApp.Service.DTOs.Department_DTOs;
using CompanyApp.Service.DTOs.Employee_DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Service.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Department, DepartmentDto>().ReverseMap();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentEditDto, Department>();

            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.DepartmentName, opt => opt.MapFrom(src => src.Department != null ? src.Department.Name : "None"))
                .ForMember(dest => dest.DepartmentId, opt => opt.MapFrom(src => src.Department != null ? src.Department.Id : (int?)null));

            CreateMap<EmployeeCreateDto, Employee>()
                .ForMember(dest => dest.Department, opt => opt.Ignore()); 

            CreateMap<EmployeeEditDto, Employee>()
                .ForMember(dest => dest.Department, opt => opt.Ignore());
        }
    }
}
