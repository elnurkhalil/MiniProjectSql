using CompanyApp.Domain.Entities;
using CompanyApp.Repository.Data;
using CompanyApp.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Repository.Repositories
{
    public abstract class BaseRepository<T> : IBaseRepository<T> where T : BaseEntity
    {
        protected readonly CompanyContext _context;

        protected BaseRepository()
        {
            _context = new CompanyContext();
        }

        public async Task<List<T>> GetAllAsync()
        {
            var prop = typeof(T).GetProperty("Department");
            if (prop != null)
            {
                return await _context.Set<T>().Include("Department").ToListAsync();
            }

            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T?> GetByIdAsync(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public async Task CreateAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        public abstract Task EditAsync(T entity);

        public Task SaveChangesAsync()
        {
            return _context.SaveChangesAsync();
        }
    }

}
