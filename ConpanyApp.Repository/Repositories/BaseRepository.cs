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

        public List<T> GetAll()
        {
            var prop = typeof(T).GetProperty("Department");
            if (prop != null)
            {
                return _context.Set<T>().Include("Department").ToList();
            }

            return _context.Set<T>().ToList();
        }

        public T? GetById(int id)
        {
            return _context.Set<T>().Find(id);
        }

        public void Create(T entity)
        {
            _context.Set<T>().Add(entity);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var entity = _context.Set<T>().Find(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                _context.SaveChanges();
            }
        }

        public abstract void Edit(T entity);
    }

}
