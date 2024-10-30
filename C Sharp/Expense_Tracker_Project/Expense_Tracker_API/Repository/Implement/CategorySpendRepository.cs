using Dapper;
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;

namespace Expense_Tracker_API.Repository.Implement
{
    public class CategorySpendRepository : ICategorySpendRepository
    {
        private readonly DapperContext _context;
        public CategorySpendRepository(UserContext context, DapperContext _context)
        {
        
            this._context = _context;
        }

        public async Task<IEnumerable<CategorySpend>> Get(int id)
        {
            var query = $"Expenses_Category {id}";

            using (var connection = _context.CreateConnection())
            {

                var entity = await connection.QueryAsync<CategorySpend>(query);
                return entity.ToList();
            }
        }

      

      


    }
}
