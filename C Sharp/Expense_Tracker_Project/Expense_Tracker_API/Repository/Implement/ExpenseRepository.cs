using Dapper;
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_API.Repository.Implement
{
    public class ExpenseRepository : IExpenseRepository
    {
        private readonly UserContext context;
        private readonly DapperContext _context;
        public ExpenseRepository(UserContext context, DapperContext _context)
        {
            this.context = context;
            this._context = _context;
        }
        
        //Get All Expense By Monthly
        public async Task<IEnumerable<Expenses>> GetMonthly(int id)
        {
            try
            {
                var query = $"Monthly_Records {id}";

                using (var connection = _context.CreateConnection())
                {

                    var entity = await connection.QueryAsync<Expenses>(query);

                    return entity.ToList();
                }
            }
            catch (Exception message)
            {
                throw message;
            }
        }  
        
        
        //Get All Expense
        public async Task<IEnumerable<Expenses>> Get(int id)
        {
            try
            {
                var query = $"AllExpense {id}";

                using (var connection = _context.CreateConnection())
                {

                    var entity = await connection.QueryAsync<Expenses>(query);

                    return entity.ToList();
                }



            }
            catch (Exception message)
            {
                throw message;
            }
        }


        //Get Expense By ID
        public async Task<Expenses> GetByID(int id)
        {
            try
            {
                var query = $"ExpenseByID {id}";

                using (var connection = _context.CreateConnection())
                {
                    var entity = await connection.QueryFirstOrDefaultAsync<Expenses>(query);

                    return  entity;
                }

            } 
            catch (Exception message)
            {
                throw message;
            }
        }


        //Delete Expense Record By ID
        public async Task Delete(int id)
        {
            try
            {
                var query = $"Delete_Expense {id}";

                using (var connection = _context.CreateConnection())
                {
                    var entity = await connection.QueryAsync<Expenses>(query);
                }
            }
            catch (Exception Message)
            {
                throw Message;
            }
        }


        public async Task<string> Add(Expenses entity)
        {
            var query = $"EXEC Insert_Expense" +
                $" @UserID = {entity.UserID}," +
                $"@CategoryID = {entity.CategoryID}," +
                $"@Amount = {entity.Amount}," +
                $"@AmountSpend = null ," +
                $"@Description = '{entity.Description}'," +
                $"@Date = '{entity.Date.ToString("yyyy-MM-dd")}'";

            using (var connection = _context.CreateConnection())
            {
                var status = await connection.ExecuteScalarAsync(query);
                return status.ToString();
            }

            
        }

        public Task Edit(Expenses entity)
        {
            throw new NotImplementedException();
        }
    }
}
