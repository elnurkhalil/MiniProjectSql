using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Repository.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<Employee>
    {
        Task<List<Employee>> GetByAgeAsync(int age);
        Task<List<Employee>> GetByDepartmentIdAsync(int departmentId);
        Task<List<Employee>> GetByDepartmentNameAsync(string departmentName);
        Task<List<Employee>> SearchByNameOrSurnameAsync(string text);
        Task<int> GetEmployeeCountAsync();
    }
}
