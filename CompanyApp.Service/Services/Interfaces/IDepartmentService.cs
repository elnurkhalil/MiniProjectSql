using CompanyApp.Domain.Enums;
using CompanyApp.Service.DTOs;
using CompanyApp.Service.DTOs.Department_DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApp.Service.Services.Interfaces;

public interface IDepartmentService
{
    Task<List<DepartmentDto>> GetAllAsync();
    Task<DepartmentDto> GetByIdAsync(int id);
    Task CreateAsync(DepartmentCreateDto dto);
    Task EditAsync(DepartmentEditDto dto);
    Task DeleteAsync(int id);
    Task<List<DepartmentDto>> SearchByNameAsync(string text);
    Task<List<DepartmentDto>> GetSortedByCapacityAsync(SortDirection direction);
}