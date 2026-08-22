using CompanyApp.ConsoleApp.Controllers;
using CompanyApp.Domain.Entities;
using CompanyApp.Domain.Enums;
using CompanyApp.Service.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompanyApp.ConsoleApp.Controllers
{
    public class DepartmentController
    {
        private readonly DepartmentService _deptService;

        public DepartmentController(DepartmentService deptService)
        {
            _deptService = deptService;
        }

        public async Task GetAllAsync()
        {
            var depts = await _deptService.GetAllAsync();
            if (!depts.Any()) Console.WriteLine("No departments found.");
            foreach (var d in depts)
                Console.WriteLine($"Id: {d.Id} | Name: {d.Name} | Capacity: {d.Capacity}");
        }

        public async Task CreateAsync()
        {
            string dname = ConsoleHelper.ReadString("Name: ", allowNumbers: false, allowPunctuation: false);
            int newDCap = ConsoleHelper.ReadNonNegativeInt("Capacity: ");

            await _deptService.CreateAsync(new Department { Name = dname, Capacity = newDCap });
            Console.WriteLine("Department created successfully.");
        }

        public async Task EditAsync()
        {
            var did = ConsoleHelper.ReadPositiveInt("Department Id to edit: ");
            var existingDept = await _deptService.GetByIdAsync(did);

            if (existingDept == null)
            {
                Console.WriteLine($"Department with Id {did} not found.");
                return;
            }

            Console.WriteLine($"Editing: {existingDept.Name} (Capacity: {existingDept.Capacity})");

            string newDName = ConsoleHelper.ReadString("New name (or press Enter to keep current): ", allowNumbers: false, allowPunctuation: false, allowEmpty: true);
            if (string.IsNullOrWhiteSpace(newDName)) newDName = existingDept.Name;

            string? newDCapInput = null;
            while (true)
            {
                Console.Write("New capacity (or press Enter to keep current): ");
                newDCapInput = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newDCapInput) || (int.TryParse(newDCapInput, out int parsedCap) && parsedCap >= 0))
                    break;
                Console.WriteLine("Please enter a valid non-negative integer.");
            }

            var deptToEdit = new Department { Id = existingDept.Id, Name = newDName, Capacity = existingDept.Capacity };
            await _deptService.EditAsync(deptToEdit, string.IsNullOrWhiteSpace(newDCapInput) ? null : newDCapInput);
            Console.WriteLine("Department edited successfully.");
        }

        public async Task DeleteAsync()
        {
            var delDid = ConsoleHelper.ReadPositiveInt("Department Id to delete: ");
            await _deptService.DeleteAsync(delDid);
            Console.WriteLine("Department deleted successfully.");
        }

        public async Task SearchByNameAsync()
        {
            string text = ConsoleHelper.ReadString("Search text (press Enter to show all): ", allowNumbers: false, allowEmpty: true);
            var found = string.IsNullOrWhiteSpace(text)
                ? await _deptService.GetAllAsync()
                : await _deptService.SearchByNameAsync(text);

            if (!found.Any()) Console.WriteLine("No departments found.");
            else foreach (var d in found) Console.WriteLine($"Id: {d.Id} | Name: {d.Name} | Capacity: {d.Capacity}");
        }

        public async Task SortByCapacityAsync()
        {
            string order = ConsoleHelper.ReadString("Order (asc/desc): ", allowEmpty: false, allowNumbers: false).ToLower();
            SortDirection dir = order == "desc" ? SortDirection.Descending : SortDirection.Ascending;

            if (order != "asc" && order != "desc") Console.WriteLine("Invalid order. Defaulting to 'asc'.");

            var sorted = await _deptService.GetSortedByCapacityAsync(dir);
            foreach (var d in sorted) Console.WriteLine($"Id: {d.Id} | Name: {d.Name} | Capacity: {d.Capacity}");
        }

        public async Task GetByIdAsync()
        {
            var id = ConsoleHelper.ReadPositiveInt("Department Id: ");
            var deptResult = await _deptService.GetByIdAsync(id);

            if (deptResult == null) Console.WriteLine("Department not found.");
            else Console.WriteLine($"Id: {deptResult.Id} | Name: {deptResult.Name} | Capacity: {deptResult.Capacity}");
        }
    }
}
