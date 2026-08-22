using Microsoft.EntityFrameworkCore;
using CompanyApp.Domain.Entities;

namespace CompanyApp.Repository.Data
{
    public class CompanyContext : DbContext
    {
        public DbSet<Department> Departments { get; set; } = null!;
        public DbSet<Employee> Employees { get; set; } = null!;

        private const string ConnectionString =
            "Server=(localdb)\\MSSQLLocalDB;Database=CompanyApp;Trusted_Connection=True;MultipleActiveResultSets=true";

        public CompanyContext() { }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer(ConnectionString);
            }
        }
    }
}
