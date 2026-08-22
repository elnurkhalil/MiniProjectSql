using CompanyApp.Domain.Entities;
using System.Collections.Generic;

namespace CompanyApp.Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        Task<System.Collections.Generic.List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task CreateAsync(Employee employee);
        Task EditAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<List<Employee>> GetByAgeAsync(int age);
        Task<List<Employee>> GetByDepartmentIdAsync(int departmentId);
        Task<List<Employee>> SearchByNameOrSurnameAsync(string text);
        Task<int> GetEmployeeCountAsync();
        public Task<List<Employee>> GetAllEmpByDepNameAsync(string departmentName);

    }
}