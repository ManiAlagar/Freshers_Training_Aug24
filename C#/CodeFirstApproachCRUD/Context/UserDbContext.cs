
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Context
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; } 
    }


}
