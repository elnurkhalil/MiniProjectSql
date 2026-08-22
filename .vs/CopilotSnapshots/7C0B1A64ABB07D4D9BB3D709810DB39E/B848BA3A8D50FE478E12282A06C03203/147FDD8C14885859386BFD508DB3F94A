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
        List<Department> SearchByName(string text);
        List<Department> GetSortedByCapacity(SortDirection direction);
    }
}
