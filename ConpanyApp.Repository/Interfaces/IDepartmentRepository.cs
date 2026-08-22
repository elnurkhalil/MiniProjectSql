using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CompanyApp.Domain.Enums;

namespace CompanyApp.Repository.Interfaces
{
    public interface IDepartmentRepository : IBaseRepository<Department>
    {
        Task<List<Department>> SearchByNameAsync(string text);
        Task<List<Department>> GetSortedByCapacityAsync(SortDirection direction);
    }
}
