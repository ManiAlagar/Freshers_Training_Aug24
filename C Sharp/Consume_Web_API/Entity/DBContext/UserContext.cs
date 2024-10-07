using Microsoft.EntityFrameworkCore;

namespace DB_First_Approach.Entity.DBContext
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> User { get; set; }
    }
}
