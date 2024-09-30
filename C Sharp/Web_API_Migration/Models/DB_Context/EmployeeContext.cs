using Microsoft.EntityFrameworkCore;

namespace Web_API_Migration.Models.DB_Context
{
    public class EmployeeContext : DbContext
    {
        public EmployeeContext(DbContextOptions options) : base(options) { }

        DbSet<Employee> Employee { get; set; }
        DbSet<Department> Department { get; set; }
    }
}


