using CompanyApp.Domain.Entities;
using CompanyApp.Service.Exceptions;
using CompanyApp.Service.Helpers.Constants;
using CompanyApp.Repository.Interfaces;
using CompanyApp.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyApp.Service.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _repository;

        public EmployeeService(IEmployeeRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task<List<Employee>> GetAllAsync() => await _repository.GetAllAsync() ?? new List<Employee>();

        public async Task<Employee> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ValidationException(Messages.IdMustBeGreaterThanZero);
            return await _repository.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(Messages.EmployeeNotFound, id));
        }

        public async Task CreateAsync(Employee employee)
        {
            if (employee == null) throw new ValidationException(Messages.EmployeeNameSurnameRequired);

            if (string.IsNullOrWhiteSpace(employee.Name) || string.IsNullOrWhiteSpace(employee.Surname))
                throw new ValidationException(Messages.EmployeeNameSurnameRequired);

            if (employee.Name.Any(char.IsDigit) || employee.Surname.Any(char.IsDigit))
                throw new ValidationException(Messages.EmployeeNameSurnameNoDigits);

            if (employee.Age < 0) throw new ValidationException(Messages.EmployeeAgeCantBeNegative);

            if (employee.Age < 18 || employee.Age > 65)
                throw new ValidationException(Messages.EmployeeAgeRestriction);

            await _repository.CreateAsync(employee);
        }

        public async Task EditAsync(Employee employee)
        {
            if (employee == null) throw new ValidationException(Messages.EmployeeNameSurnameRequired);
            if (employee.Id <= 0) throw new ValidationException(Messages.IdMustBeGreaterThanZero);

            var existing = await _repository.GetByIdAsync(employee.Id);
            if (existing == null) throw new NotFoundException(string.Format(Messages.EmployeeNotFound, employee.Id));

            if (string.IsNullOrWhiteSpace(employee.Name))
            {
                employee.Name = existing.Name;
            }
            else
            {
                if (employee.Name.Any(char.IsDigit))
                    throw new ValidationException(Messages.EmployeeNameSurnameNoDigits);
            }

            if (string.IsNullOrWhiteSpace(employee.Surname))
            {
                employee.Surname = existing.Surname;
            }
            else
            {
                if (employee.Surname.Any(char.IsDigit))
                    throw new ValidationException(Messages.EmployeeNameSurnameNoDigits);
            }

            if (employee.Age <= 0)
            {
                employee.Age = existing.Age;
            }
            else
            {
                if (employee.Age < 18 || employee.Age > 65)
                    throw new ValidationException(Messages.EmployeeAgeRestriction);
            }

            if (string.IsNullOrWhiteSpace(employee.Address))
            {
                employee.Address = existing.Address;
            }

            if (employee.Department == null)
            {
                employee.Department = existing.Department;
            }

            await _repository.EditAsync(employee);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            if (id <= 0) throw new ValidationException(Messages.IdMustBeGreaterThanZero);
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException(string.Format(Messages.EmployeeNotFound, id));
            await _repository.DeleteAsync(id);
        }

        public async Task<List<Employee>> GetByAgeAsync(int age) => await _repository.GetByAgeAsync(age) ?? new List<Employee>();

        public async Task<List<Employee>> GetByDepartmentIdAsync(int departmentId) => await _repository.GetByDepartmentIdAsync(departmentId) ?? new List<Employee>();

        public async Task<List<Employee>> SearchByNameOrSurnameAsync(string text) => await _repository.SearchByNameOrSurnameAsync(text) ?? new List<Employee>();

        public async Task<int> GetEmployeeCountAsync() => await _repository.GetEmployeeCountAsync();

        public async Task<List<Employee>> GetAllEmpByDepNameAsync(string departmentName)
        {
            if (string.IsNullOrWhiteSpace(departmentName))
                return new List<Employee>();

            return await _repository.GetByDepartmentNameAsync(departmentName) ?? new List<Employee>();
        }
    }
}