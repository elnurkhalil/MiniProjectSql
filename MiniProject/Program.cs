using CompanyApp.ConsoleApp.Controllers;
using CompanyApp.Repository.Repositories;
using CompanyApp.Service.Exceptions;
using CompanyApp.Service.Services;
using ConpanyApp.Service.Services;
using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        var deptRepo = new DepartmentRepository();
        var empRepo = new EmployeeRepository();

        var deptService = new DepartmentService(deptRepo);
        var empService = new EmployeeService(empRepo);

        var deptController = new DepartmentController(deptService);
        var empController = new EmployeeController(empService, deptService);

        while (true)
        {
            PrintMenu();
            var key = Console.ReadLine()?.Trim();

            try
            {
                if (key == "0")
                {
                    Console.WriteLine("Exiting program...");
                    break;
                }

                switch (key)
                {
                    case "1": await deptController.GetAllAsync(); break;
                    case "2": await deptController.CreateAsync(); break;
                    case "3": await deptController.EditAsync(); break;
                    case "4": await deptController.DeleteAsync(); break;
                    case "5": await deptController.SearchByNameAsync(); break;
                    case "6": await deptController.SortByCapacityAsync(); break;
                    case "15": await deptController.GetByIdAsync(); break;

                    case "7": await empController.GetAllAsync(); break;
                    case "8": await empController.CreateAsync(); break;
                    case "9": await empController.EditAsync(); break;
                    case "10": await empController.DeleteAsync(); break;
                    case "11": await empController.GetByAgeAsync(); break;
                    case "12": await empController.GetByDepartmentIdAsync(); break;
                    case "13": await empController.SearchAsync(); break;
                    case "14": await empController.PrintCountAsync(); break;
                    case "16": await empController.GetByIdAsync(); break;
                    case "17": await empController.GetByDepartmentNameAsync(); break;

                    default:
                        Console.WriteLine("Unknown option. Please try again.");
                        break;
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"\n[Validation Error]: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\n[System Error]: {ex.Message}");
            }
        }
    }

    static void PrintMenu()
    {
        Console.WriteLine("\n=== Company App ===");
        Console.WriteLine("1.  List all departments");
        Console.WriteLine("2.  Create department");
        Console.WriteLine("3.  Edit department");
        Console.WriteLine("4.  Delete department");
        Console.WriteLine("5.  Search departments by name");
        Console.WriteLine("6.  Sort departments by capacity (asc/desc)");
        Console.WriteLine("7.  List all employees");
        Console.WriteLine("8.  Create employee");
        Console.WriteLine("9.  Edit employee");
        Console.WriteLine("10. Delete employee");
        Console.WriteLine("11. Get employees by age");
        Console.WriteLine("12. Get employees by department id");
        Console.WriteLine("13. Search employees by name or surname");
        Console.WriteLine("14. Employees count");
        Console.WriteLine("15. Get department by id");
        Console.WriteLine("16. Get employee by id");
        Console.WriteLine("17. Get all employees by department name");
        Console.WriteLine("0.  Exit");
        Console.WriteLine("===================");
        Console.Write("Select: ");
    }
}