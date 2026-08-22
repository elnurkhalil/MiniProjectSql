using CompanyApp.Domain.Entities;
using System.Collections.Generic;

namespace CompanyApp.Service.Services.Interfaces
{
    public interface IEmployeeService
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetAllAsync();
        System.Threading.Tasks.Task<Employee> GetByIdAsync(int id);
        System.Threading.Tasks.Task CreateAsync(Employee employee);
        System.Threading.Tasks.Task EditAsync(Employee employee);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetByAgeAsync(int age);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetByDepartmentIdAsync(int departmentId);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> SearchByNameOrSurnameAsync(string text);
        System.Threading.Tasks.Task<int> GetEmployeeCountAsync();
        public System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetAllEmpByDepNameAsync(string departmentName);

    }
}