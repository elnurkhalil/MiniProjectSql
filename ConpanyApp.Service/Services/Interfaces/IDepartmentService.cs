using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using System.Collections.Generic;

namespace CompanyApp.Service.Services.Interfaces
{
    public interface IDepartmentService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> GetAllAsync();
        System.Threading.Tasks.Task<Department> GetByIdAsync(int id);
        System.Threading.Tasks.Task CreateAsync(Department department);
        System.Threading.Tasks.Task EditAsync(Department department, string? capacityInput);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> SearchByNameAsync(string text);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> GetSortedByCapacityAsync(SortDirection direction);
    }
}