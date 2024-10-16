using BookstoreApplication.Models;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Context
{
    public class BookDBContext: DbContext
    {
        public BookDBContext(DbContextOptions options):base(options) { }
        public DbSet<Book>Books { get; set; }
        public DbSet<Role> Roles { get; set; } 
        public DbSet<User> Users { get; set; }
        public DbSet<Order> Orders { get; set; }    
        public DbSet<Status> Statuses { get; set; }
    }
}
