using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using CompanyApp.Repository.Data;
using CompanyApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanyApp.Repository.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository() : base() { }

        public override void Edit(Department entity)
        {
            var existing = _context.Departments.Find(entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Capacity = entity.Capacity;
                _context.SaveChanges();
            }
        }

        public List<Department> SearchByName(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return new List<Department>();
            return _context.Departments
                .Where(d => d.Name != null && d.Name.Contains(text.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<Department> GetSortedByCapacity(SortDirection direction)
        {
            var query = _context.Departments.AsQueryable();
            if (direction == SortDirection.Ascending)
            {
                return query.OrderBy(d => d.Capacity ?? int.MaxValue).ToList();
            }

            return query.OrderByDescending(d => d.Capacity ?? int.MinValue).ToList();
        }
    }
}
