using Dapper;
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.AspNetCore.Http.HttpResults;
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
                var entity = await GetByID(id);

                context.Expenses.Remove(entity);
                await context.SaveChangesAsync();
            }
            catch (Exception Message)
            {
                throw Message;
            }
        }


        public Task Add(Expenses entity)
        {
            throw new NotImplementedException();
        }

        public Task Edit(Expenses entity)
        {
            throw new NotImplementedException();
        }
    }
}
