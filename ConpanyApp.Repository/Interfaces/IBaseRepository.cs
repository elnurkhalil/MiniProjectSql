using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.Repository.Interfaces
{
    public interface IBaseRepository<T> where T : BaseEntity
    {
        System.Threading.Tasks.Task<System.Collections.Generic.List<T>> GetAllAsync();
        System.Threading.Tasks.Task<T?> GetByIdAsync(int id);
        System.Threading.Tasks.Task CreateAsync(T entity);
        System.Threading.Tasks.Task EditAsync(T entity);
        System.Threading.Tasks.Task DeleteAsync(int id);
        System.Threading.Tasks.Task SaveChangesAsync();
    }
}
