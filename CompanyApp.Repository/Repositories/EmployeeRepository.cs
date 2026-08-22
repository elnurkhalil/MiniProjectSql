using CompanyApp.Domain.Entities;
using CompanyApp.Repository.Data;
using CompanyApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository() : base() { }
        public override async Task EditAsync(Employee entity)
        {
            var existing = await _context.Employees.FindAsync(entity.Id);
            if (existing == null) return;

            existing.Name = entity.Name;
            existing.Surname = entity.Surname;
            existing.Age = entity.Age;
            existing.Address = entity.Address;
            existing.Department = entity.Department;
        }

        public async Task<List<Employee>> GetByAgeAsync(int age)
        {
            if (age <= 0) return new List<Employee>();
            return await _context.Employees.Where(e => e.Age == age).ToListAsync();
        }

        public async Task<List<Employee>> GetByDepartmentIdAsync(int departmentId)
        {
            if (departmentId <= 0) return new List<Employee>();
            return await _context.Employees.Where(e => e.Department != null && e.Department.Id == departmentId).ToListAsync();
        }

        public async Task<List<Employee>> GetByDepartmentNameAsync(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName)) return new List<Employee>();

            return await _context.Employees
                .Where(e => e.Department != null &&
                            e.Department.Name.Equals(departmentName.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<Employee>> SearchByNameOrSurnameAsync(string text)
        {
            text = text?.Trim();
            if (string.IsNullOrEmpty(text)) return new List<Employee>();
            return await _context.Employees.Where(e => (!string.IsNullOrWhiteSpace(e.Name) && e.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrWhiteSpace(e.Surname) && e.Surname.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)).ToListAsync();
        }

        public Task<int> GetEmployeeCountAsync() => _context.Employees.CountAsync();
    }
}
