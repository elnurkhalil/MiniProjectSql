using CompanyApp.Service.DTOs;
using CompanyApp.Service.DTOs.Employee_DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CompanyApp.Service.Services.Interfaces;

public interface IEmployeeService
{
    Task<List<EmployeeDto>> GetAllAsync();
    Task<EmployeeDto> GetByIdAsync(int id);
    Task CreateAsync(EmployeeCreateDto dto);
    Task EditAsync(EmployeeEditDto dto);
    Task DeleteAsync(int id);
    Task<List<EmployeeDto>> GetByAgeAsync(int age);
    Task<List<EmployeeDto>> GetByDepartmentIdAsync(int departmentId);
    Task<List<EmployeeDto>> SearchByNameOrSurnameAsync(string text);
    Task<int> GetEmployeeCountAsync();
    Task<List<EmployeeDto>> GetAllEmpByDepNameAsync(string departmentName);
}