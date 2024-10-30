using Dapper;
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using UserConfiguration = Expense_Tracker_API.Entity.UserConfiguration;

namespace Expense_Tracker_API.Repository.Implement
{
    public class ConfigurationRepository : IConfigurationRepository
    {
        private readonly UserContext context;
        private readonly DapperContext _context;

        public ConfigurationRepository(UserContext context, DapperContext _context)
        {
            this.context = context;
            this._context = _context;
        }



        public async Task<IEnumerable<UserConfiguration>> Get(int id)
        {
            //return entity;
            //var query = $"All_Configuration {id}";

            //using (var connection = _context.CreateConnection())
            //{
            //    var entity = await connection.QueryAsync<UserConfiguartion>(query);
            //    return entity.ToList();
            //}

            var entity = await context.UserConfiguration.ToListAsync();
            return entity;
        }

        public async Task<UserConfiguration> GetById(int id)
        {
            var entity = await context.UserConfiguration.FirstOrDefaultAsync(u => u.Id == id);
            return entity;
        }




        public async Task Add(UserConfiguration entity)
        {
            var query = $"EXEC Insert_Configuration" +
                $" @UserID = {entity.UserID}," +
                $"@ConfigKey= '{entity.ConfigKey}'," +
                $"@ConfigValue = '{entity.ConfigValue}'";

            using (var connection = _context.CreateConnection())
            {
                var status = await connection.ExecuteAsync(query);
            }
        }

        public async Task Edit(UserConfiguration entity)
        {
            var query = $"EXEC Update_Configuration" +
                 $" @ID = {entity.Id}," +
                $" @UserID = {entity.UserID}," +
                $"@ConfigKey= '{entity.ConfigKey}'," +
                $"@ConfigValue = '{entity.ConfigValue}'"; 

            using (var connection = _context.CreateConnection())
            {
                var status = await connection.ExecuteAsync(query);
            }
            await context.SaveChangesAsync();
        }
    }
}
