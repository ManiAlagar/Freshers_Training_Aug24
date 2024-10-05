using Microsoft.EntityFrameworkCore;
using System;

namespace Student_CRUD_API.Entity.DBContext
{
    public class StudentContext : DbContext
    {
        public StudentContext(DbContextOptions<StudentContext> options) : base(options) { }

        public DbSet<Students> Student { get; set; }
    }
}
