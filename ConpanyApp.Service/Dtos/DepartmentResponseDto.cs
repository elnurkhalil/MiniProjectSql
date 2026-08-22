using System;

namespace ConpanyApp.Service.Dtos
{
    public class DepartmentResponseDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int? Capacity { get; set; }
    }
}
