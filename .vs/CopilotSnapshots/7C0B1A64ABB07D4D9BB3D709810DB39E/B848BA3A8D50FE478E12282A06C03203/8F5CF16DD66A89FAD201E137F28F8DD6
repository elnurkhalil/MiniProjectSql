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
        List<Employee> GetByAge(int age);
        List<Employee> GetByDepartmentId(int departmentId);
        List<Employee> GetByDepartmentName(string departmentName);
        List<Employee> SearchByNameOrSurname(string text);
        int GetEmployeeCount();
    }
}
