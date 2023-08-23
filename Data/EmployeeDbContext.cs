using Microsoft.EntityFrameworkCore;
using SampleWeb.API.Models;

namespace SampleWeb.API.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions<EmployeeDbContext> options) : base(options)
        {
            
        }

        public DbSet<Employee> Employee { get; set; }
    }
}
