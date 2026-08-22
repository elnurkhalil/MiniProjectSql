using CompanyApp.ConsoleApp.Controllers;
using CompanyApp.Domain.Entities;
using CompanyApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.ConsoleApp.Controllers
{
    public class EmployeeController
    {
        private readonly EmployeeService _empService;
        private readonly DepartmentService _deptService;

        public EmployeeController(EmployeeService empService, DepartmentService deptService)
        {
            _empService = empService;
            _deptService = deptService;
        }

        public async Task GetAllAsync()
        {
            var emps = await _empService.GetAllAsync();
            if (!emps.Any()) Console.WriteLine("No employees found.");
            foreach (var e in emps) PrintEmployee(e);
        }

        public async Task CreateAsync()
        {
            var name = ConsoleHelper.ReadString("Name: ", allowNumbers: false, allowEmpty: false);
            var surname = ConsoleHelper.ReadString("Surname: ", allowNumbers: false, allowEmpty: false);

            int age;
            while (true)
            {
                age = ConsoleHelper.ReadNonNegativeInt("Age: ");
                if (age >= 18 && age <= 65) break;
                Console.WriteLine("Error: Age must be between 18 and 65 years. Please try again.");
            }

            var addr = ConsoleHelper.ReadString("Address: ", allowEmpty: false);
            var deptId = ConsoleHelper.ReadNonNegativeInt("Department id (or 0 to skip): ");

            Department? dept = null;
            if (deptId > 0)
            {
                dept = await _deptService.GetByIdAsync(deptId);
                if (dept == null) Console.WriteLine("Warning: Department not found. Employee will have no department.");
            }

            await _empService.CreateAsync(new Employee { Name = name, Surname = surname, Age = age, Address = addr, Department = dept! });
            Console.WriteLine("Employee created successfully.");
        }

        public async Task EditAsync()
        {
            var id = ConsoleHelper.ReadPositiveInt("Employee Id to edit: ");
            var existingEmp = await _empService.GetByIdAsync(id);

            if (existingEmp == null)
            {
                Console.WriteLine("Employee not found.");
                return;
            }

            var ename = ConsoleHelper.ReadString($"Name: ", allowNumbers: false, allowEmpty: true);
            var esurname = ConsoleHelper.ReadString($"Surname: ", allowNumbers: false, allowEmpty: true);

            Console.Write($"Age: ");
            var ageStr = Console.ReadLine();

            var eaddr = ConsoleHelper.ReadString($"Address: ", allowEmpty: true);
            var edeptIdStr = ConsoleHelper.ReadString($"Department id: ", allowEmpty: true);

            Department? finalDept = existingEmp.Department;
            if (!string.IsNullOrWhiteSpace(edeptIdStr) && int.TryParse(edeptIdStr, out int newDeptId))
            {
                finalDept = newDeptId > 0 ? await _deptService.GetByIdAsync(newDeptId) : null;
            }

            await _empService.EditAsync(new Employee
            {
                Id = id,
                Name = string.IsNullOrWhiteSpace(ename) ? existingEmp.Name : ename,
                Surname = string.IsNullOrWhiteSpace(esurname) ? existingEmp.Surname : esurname,
                Age = string.IsNullOrWhiteSpace(ageStr) ? existingEmp.Age : int.Parse(ageStr),
                Address = string.IsNullOrWhiteSpace(eaddr) ? existingEmp.Address : eaddr,
                Department = finalDept!
            });

            Console.WriteLine("Employee edited successfully.");
        }

        public async Task DeleteAsync()
        {
            var delId = ConsoleHelper.ReadPositiveInt("Employee Id to delete: ");
            await _empService.DeleteAsync(delId);
            Console.WriteLine("Employee deleted successfully.");
        }

        public async Task GetByAgeAsync()
        {
            var searchAge = ConsoleHelper.ReadPositiveInt("Age: ");
            var byAge = await _empService.GetByAgeAsync(searchAge);
            if (!byAge.Any()) Console.WriteLine("No employees found with this age.");
            foreach (var e in byAge) PrintEmployee(e);
        }

        public async Task GetByDepartmentIdAsync()
        {
            var searchDeptId = ConsoleHelper.ReadPositiveInt("Department id: ");
            var byDept = await _empService.GetByDepartmentIdAsync(searchDeptId);
            if (!byDept.Any()) Console.WriteLine("No employees found in this department.");
            foreach (var e in byDept) PrintEmployee(e);
        }

        public async Task SearchAsync()
        {
            var stext = ConsoleHelper.ReadString("Search text: ", allowEmpty: false);
            var se = await _empService.SearchByNameOrSurnameAsync(stext);
            if (!se.Any()) Console.WriteLine("No employees found.");
            foreach (var e in se) PrintEmployee(e);
        }

        public async Task PrintCountAsync()
        {
            Console.WriteLine($"Total employees: {await _empService.GetEmployeeCountAsync()}");
        }

        public async Task GetByIdAsync()
        {
            var searchEmpId = ConsoleHelper.ReadPositiveInt("Employee Id: ");
            var empResult = await _empService.GetByIdAsync(searchEmpId);

            if (empResult == null) Console.WriteLine("Employee not found.");
            else PrintEmployee(empResult);
        }

        public async Task GetByDepartmentNameAsync()
        {
            var depName = ConsoleHelper.ReadString("Department name: ", allowNumbers: true, allowEmpty: false);
            var empsByDepName = await _empService.GetAllEmpByDepNameAsync(depName);

            if (!empsByDepName.Any()) Console.WriteLine($"No employees found in department '{depName}'.");
            else foreach (var e in empsByDepName) PrintEmployee(e);
        }

        private void PrintEmployee(Employee e)
        {
            Console.WriteLine($"Id: {e.Id} | {e.Name} {e.Surname} | Age: {e.Age} | Dept: {e.Department?.Name ?? "None"}");
        }
    }
}
