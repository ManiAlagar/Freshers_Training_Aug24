using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_API.Entity.DBContext
{
    public class UserContext: DbContext
    {
        public UserContext(DbContextOptions options) : base(options) { }
        public DbSet<Users> Users { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Budget> Budget { get; set; }
        public DbSet<Expenses> Expenses { get; set; }
        public DbSet<UserConfiguration> UserConfiguration { get; set; }
        public IEnumerable<object> CategorySpend { get; internal set; }
    }
}
