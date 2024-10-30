using Dapper;
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using static Dapper.SqlMapper;


namespace Expense_Tracker_API.Repository.Implement
{
    public class BudgetRepository : IBudgetRepository
    {
        private readonly UserContext context;
        private readonly DapperContext _context;
        public BudgetRepository(UserContext context, DapperContext _context)
        {
            this.context = context;
            this._context = _context;
        }

        public async Task<IEnumerable<Budget>> Get(int id)
        {
            //var entity = await context.Budget.Where(e => e.UserID == id).ToListAsync();
            //return entity;
            var query = $"AllBudget {id}";

            using (var connection = _context.CreateConnection())
            {

                var entity = await connection.QueryAsync<Budget>(query);
                return entity.ToList();
            }
        }


        public async Task<Budget> GetByID(int id)
        {   
            var query = $"BudgetByID {id}";

            using (var connection = _context.CreateConnection())
            {
                var entity = await connection.QueryFirstOrDefaultAsync<Budget>(query);
                return entity;
            }
        }

       

        public async Task<string> Add(Budget entity)
        {

            var query = $"EXEC Insert_Budget" +
                $" @UserID = {entity.UserID}," +
                $"@CategoryID = {entity.CategoryID}," +
                $"@BudgetAmount = {entity.BudgetAmount}," +
                $"@Balance = {entity.Balance}," +
                $"@StartDate = '{entity.StartDate.ToString("yyyy-MM-dd")}'," +
                $"@EndDate = '{entity.EndDate.ToString("yyyy-MM-dd")}'," +
                $"@TimeFrame = '{entity.TimeFrame}'";

            using (var connection = _context.CreateConnection())
            {
                var status = await connection.ExecuteScalarAsync(query);
                return status.ToString();
            }
        }

        public async  Task<string> Edit(Budget entity)
        {
            var query = $"EXEC Update_Budget" +
              $" @ID = {entity.Id}," + 
              $" @UserID = {entity.UserID}," +
              $"@CategoryID = {entity.CategoryID}," +
              $"@BudgetAmount = {entity.BudgetAmount}," +
              $"@Balance = {entity.Balance}," +
              $"@StartDate = '{entity.StartDate.ToString("yyyy-MM-dd")}'," +
              $"@EndDate = '{entity.EndDate.ToString("yyyy-MM-dd")}'," +
              $"@TimeFrame = '{entity.TimeFrame}'";

            using (var connection = _context.CreateConnection())
            {
                var status = await connection.ExecuteScalarAsync(query);
                return status.ToString();
            }
             //await context.SaveChangesAsync();
        }

        public async Task<string> Delete(int id)
        {
            try
            {
                var query = $"Delete_Budget {id}";

                using (var connection = _context.CreateConnection())
                {
                    var status = await connection.ExecuteScalarAsync(query);

                    return status.ToString();
                }
            }
            catch (Exception Message)
            {
                throw Message;
            }

        }
    }
}
