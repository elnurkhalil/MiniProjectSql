using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using System.Collections.Generic;

namespace CompanyApp.Service.Services.Interfaces
{
    public interface IDepartmentService
    {
        Task<List<Department>> GetAllAsync();
        Task<Department> GetByIdAsync(int id);
        Task CreateAsync(Department department);
        Task EditAsync(Department department, string? capacityInput);
        Task DeleteAsync(int id);
        Task<List<Department>> SearchByNameAsync(string text);
        Task<List<Department>> GetSortedByCapacityAsync(SortDirection direction);
    }
}