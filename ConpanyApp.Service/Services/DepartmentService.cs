using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using CompanyApp.Repository.Interfaces;
using CompanyApp.Service.Exceptions;
using CompanyApp.Service.Helpers.Constants;
using CompanyApp.Service.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Formats.Tar;
using System.Linq;
using System.Xml.Linq;

namespace ConpanyApp.Service.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _repo;

        public DepartmentService(IDepartmentRepository repo)
        {
            _repo = repo ?? throw new ArgumentNullException(nameof(repo));
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> GetAllAsync()
        {
            var departments = await _repo.GetAllAsync();

            if (departments == null || departments.Count == 0)
            {
                throw new NotFoundException(Messages.DontHaveAnyDepartments);
            }

            return departments;
        }

        public async System.Threading.Tasks.Task<Department> GetByIdAsync(int id)
        {
            if (id <= 0) throw new ValidationException(Messages.IdMustBeGreaterThanZero);

            return await _repo.GetByIdAsync(id) ?? throw new NotFoundException(string.Format(Messages.DepartmentNotFound, id));
        }

        public async System.Threading.Tasks.Task CreateAsync(Department department)
        {
            if (department == null)
                throw new ValidationException(Messages.DepartmentMustNotBeNull);

            ValidateName(department.Name);

            var existingList = await _repo.GetAllAsync();
            var isDuplicate = existingList?.Any(d => d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)) ?? false;

            if (isDuplicate)
            {
                throw new ValidationException(Messages.SameDepartmentName);
            }

            if (department.Capacity < 0)
                throw new ValidationException(Messages.DepartmentCapacityNonNegative);

            await _repo.CreateAsync(department);
        }

        public async System.Threading.Tasks.Task EditAsync(Department department, string? capacityInput)
        {
            if (department == null) throw new ValidationException(Messages.DepartmentMustNotBeNull);

            var existing = await GetByIdAsync(department.Id);

            if (!string.IsNullOrWhiteSpace(department.Name))
            {
                ValidateName(department.Name);

                var existingList = await _repo.GetAllAsync();
                var isDuplicate = existingList?.Any(d => d.Id != department.Id && d.Name.Equals(department.Name, StringComparison.OrdinalIgnoreCase)) ?? false;

                if (isDuplicate)
                {
                    throw new ValidationException(Messages.SameDepartmentName);
                }

                existing.Name = department.Name;
            }

            if (!string.IsNullOrWhiteSpace(capacityInput))
            {
                if (!int.TryParse(capacityInput.Trim(), out var parsed) || parsed < 0)
                    throw new ValidationException(Messages.DepartmentCapacityInvalidNumber);

                existing.Capacity = parsed;
            }

            await _repo.EditAsync(existing);
        }

        public async System.Threading.Tasks.Task DeleteAsync(int id)
        {
            var existing = await GetByIdAsync(id);
            await _repo.DeleteAsync(id);
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> SearchByNameAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text))
            {
                return await _repo.GetAllAsync() ?? new System.Collections.Generic.List<Department>();
            }

            return await _repo.SearchByNameAsync(text) ?? new System.Collections.Generic.List<Department>();
        }

        public async System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> GetSortedByCapacityAsync(SortDirection direction)
        {
            return await _repo.GetSortedByCapacityAsync(direction) ?? new System.Collections.Generic.List<Department>();
        }

        private void ValidateName(string? name)
        {
            if (string.IsNullOrWhiteSpace(name))
                throw new ValidationException(Messages.DepartmentNameRequired);

            if (name.Any(char.IsDigit))
                throw new ValidationException(Messages.DepartmentNameNoDigits);
        }
    }
}