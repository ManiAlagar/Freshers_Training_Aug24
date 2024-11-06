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

        public async Task<Config> GetConfigById(int Id)
        {
            var val = await db.Config.FindAsync(Id);
            return val;
        }

        public async Task<int> UpdateConfig(int Id, Config config)
        {
            
            var existing = await db.Config.FindAsync(Id);
            if (existing != null)
            {
                existing.ConfigValue = config.ConfigValue;
                await db.SaveChangesAsync();
                return 1;
            }
            else
            {
                return 0;
            }

        }

    }
}
