using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository.Implementation
{
    public class ConfigRepository : IConfigRepository
    {
        public readonly BookDBContext db;
        
        public ConfigRepository(BookDBContext context)
        {
            this.db = context;
        }
        public async Task<IEnumerable<Config>> GetAllConfigValues()
        {
            try
            {
                return await db.Config.ToListAsync();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
