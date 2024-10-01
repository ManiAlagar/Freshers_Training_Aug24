using CodeFirstApproach.Models;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproach.Context
{
    public class StudentDbContext : DbContext //collection of tables
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; } // a table-  Querying Data
        public DbSet<Mark> Marks { get; set; }
    }
}
