using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository.Implementation
{
    public class StatusRepository:IStatusRepository
    {
        public readonly BookDBContext db;

        public StatusRepository(BookDBContext context)
        {
            db = context;
        }

        public async Task<IEnumerable<Status>> GetAllOrderStatus()
        {
            return await db.Statuses.ToListAsync();
        }
    }
}
