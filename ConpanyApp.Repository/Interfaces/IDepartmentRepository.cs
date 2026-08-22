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
        System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> SearchByNameAsync(string text);
        System.Threading.Tasks.Task<System.Collections.Generic.List<Department>> GetSortedByCapacityAsync(SortDirection direction);
    }
}
