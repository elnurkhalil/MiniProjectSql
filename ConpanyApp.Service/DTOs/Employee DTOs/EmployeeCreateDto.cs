namespace CompanyApp.Service.DTOs.Employee_DTOs
{
    public class EmployeeCreateDto
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public int Age { get; set; }
        public string Address { get; set; } = string.Empty;
        public int? DepartmentId { get; set; }
    }
}
