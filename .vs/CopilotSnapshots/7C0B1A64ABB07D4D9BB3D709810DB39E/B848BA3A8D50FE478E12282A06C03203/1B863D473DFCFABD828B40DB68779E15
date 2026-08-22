using CompanyApp.Domain.Entities;
using CompanyApp.Repository.Data;
using CompanyApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Repository.Repositories
{
    public class EmployeeRepository : BaseRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository() : base() { }

        public override void Edit(Employee entity)
        {
            var existing = _context.Employees.Find(entity.Id);
            if (existing == null) return;

            existing.Name = entity.Name;
            existing.Surname = entity.Surname;
            existing.Age = entity.Age;
            existing.Address = entity.Address;
            existing.Department = entity.Department;
            _context.SaveChanges();
        }

        public List<Employee> GetByAge(int age)
        {
            if (age <= 0) return new List<Employee>();
            return _context.Employees.Where(e => e.Age == age).ToList();
        }

        public List<Employee> GetByDepartmentId(int departmentId)
        {
            if (departmentId <= 0) return new List<Employee>();
            return _context.Employees.Where(e => e.Department != null && e.Department.Id == departmentId).ToList();
        }
        public List<Employee> GetByDepartmentName(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName)) return new List<Employee>();

            return _context.Employees
                .Where(e => e.Department != null &&
                            e.Department.Name.Equals(departmentName.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Employee> SearchByNameOrSurname(string text)
        {
            text = text?.Trim();
            if (string.IsNullOrEmpty(text)) return new List<Employee>();
            return _context.Employees.Where(e => (!string.IsNullOrWhiteSpace(e.Name) && e.Name.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0) || (!string.IsNullOrWhiteSpace(e.Surname) && e.Surname.IndexOf(text, StringComparison.OrdinalIgnoreCase) >= 0)).ToList();
        }

        public int GetEmployeeCount() => _context.Employees.Count();
    }
}
