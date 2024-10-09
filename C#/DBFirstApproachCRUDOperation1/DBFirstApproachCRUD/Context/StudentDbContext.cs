using DBFirstApproachCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproachCRUD.Context
{
    public class StudentDbContext : DbContext
    {
        public StudentDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Student> Students { get; set; }
    }
}
