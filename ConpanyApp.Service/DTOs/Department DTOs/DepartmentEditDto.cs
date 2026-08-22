namespace CompanyApp.Service.DTOs.Department_DTOs
{
    public class DepartmentEditDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Capacity { get; set; }
    }
}
