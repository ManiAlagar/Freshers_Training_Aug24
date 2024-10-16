using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository.Implementation
{
    public class RoleRepository:IRoleRepository
    {
        public readonly BookDBContext db;

        public RoleRepository(BookDBContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await db.Roles.ToListAsync();
        }
    }
}
