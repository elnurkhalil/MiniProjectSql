using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using CompanyApp.Repository.Data;
using CompanyApp.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace CompanyApp.Repository.Repositories
{
    public class DepartmentRepository : BaseRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository() : base() { }

        public override async Task EditAsync(Department entity)
        {
            var existing = await _context.Departments.FindAsync(entity.Id);
            if (existing != null)
            {
                existing.Name = entity.Name;
                existing.Capacity = entity.Capacity;
            }
        }

        public async Task<List<Department>> SearchByNameAsync(string text)
        {
            if (string.IsNullOrWhiteSpace(text)) return new List<Department>();
            return await _context.Departments
                .Where(d => d.Name != null && d.Name.Contains(text.Trim(), StringComparison.OrdinalIgnoreCase))
                .ToListAsync();
        }

        public async Task<List<Department>> GetSortedByCapacityAsync(SortDirection direction)
        {
            var query = _context.Departments.AsQueryable();
            if (direction == SortDirection.Ascending)
            {
                return await query.OrderBy(d => d.Capacity ?? int.MaxValue).ToListAsync();
            }

            return await query.OrderByDescending(d => d.Capacity ?? int.MinValue).ToListAsync();
        }
    }
}
