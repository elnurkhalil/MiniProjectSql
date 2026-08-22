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
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetByAgeAsync(int age);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetByDepartmentIdAsync(int departmentId);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> GetByDepartmentNameAsync(string departmentName);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Employee>> SearchByNameOrSurnameAsync(string text);
        System.Threading.Tasks.Task<int> GetEmployeeCountAsync();
    }
}
