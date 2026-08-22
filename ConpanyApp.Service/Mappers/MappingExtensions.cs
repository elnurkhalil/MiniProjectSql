using CompanyApp.Domain.Entities;
using ConpanyApp.Service.DTOs;

namespace ConpanyApp.Service.Mappers
{
    public static class MappingExtensions
    {
        public static DepartmentResponseDto ToResponseDto(this Department d)
        {
            if (d == null) return null!;
            return new DepartmentResponseDto
            {
                Id = d.Id,
                Name = d.Name,
                Capacity = d.Capacity
            };
        }

        public static Department ToEntity(this CreateDepartmentDto dto)
        {
            if (dto == null) return null!;
            return new Department
            {
                Name = dto.Name,
                Capacity = dto.Capacity
            };
        }

        public static EmployeeResponseDto ToResponseDto(this Employee e)
        {
            if (e == null) return null!;
            return new EmployeeResponseDto
            {
                Id = e.Id,
                Name = e.Name,
                Surname = e.Surname,
                Age = e.Age,
                Address = e.Address,
                DepartmentId = e.Department?.Id ?? 0,
                DepartmentName = e.Department?.Name ?? string.Empty
            };
        }

        public static Employee ToEntity(this CreateEmployeeDto dto, Department? department = null)
        {
            if (dto == null) return null!;
            return new Employee
            {
                Name = dto.Name,
                Surname = dto.Surname,
                Age = dto.Age,
                Address = dto.Address,
                Department = department ?? new Department { Id = dto.DepartmentId }
            };
        }
    }
}
