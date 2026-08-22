using CompanyApp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text; 
using System.Threading.Tasks;

namespace CompanyApp.Repository.Data
{
    public static class AppDbContext
    {
        public static List<Department> Departments { get; set; } = new();
        public static List<Employee> Employees { get; set; } = new();

        private static int _departmentIdCounter = 1;
        private static int _employeeIdCounter = 1;

        public static int GetNextDepartmentId() => _departmentIdCounter++;
        public static int GetNextEmployeeId() => _employeeIdCounter++;
    }
}
