using System;

namespace ConpanyApp.Service.DTOs
{
    public class CreateDepartmentDto
    {
        public string Name { get; set; } = string.Empty;
        public int? Capacity { get; set; }
    }
}
