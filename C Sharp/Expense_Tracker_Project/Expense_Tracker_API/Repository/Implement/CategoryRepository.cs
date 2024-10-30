using Dapper;
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_API.Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UserContext context;
        private readonly DapperContext _context;
        public CategoryRepository(UserContext context, DapperContext _context)
        {
            this.context = context;
            this._context = _context;
        }

        
        public async Task<IEnumerable<Category>> Get(int id)
        {
            var entity = await context.Category.Where(e => e.UserId == id).ToListAsync();
            return  entity;
        }

        public async Task<Category> GetByID(int id)
        {
            var entity = await context.Category.FirstOrDefaultAsync(e => e.CategoryID == id);
            return entity;
        }

        public async Task<string> Add(Category entity)
        {
            var query = $"EXEC Insert_Category" +
            $" @UserID = {entity.UserId}," +
            $"@CategoryName = '{entity.@CategoryName}'";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync(query);
                return result.ToString();
            }
        }

        public async Task<string> Edit(Category entity)
        {
            Category category = await GetByID(entity.CategoryID);

            //if (category == null)
            //{
            //    category.CategoryName = entity.CategoryName;
            //    context.Category.Update(category);
            //    await context.SaveChangesAsync();

            //    return "Success";
            //}
            //else
            //    return "Failure";

            var query = $"EXEC Update_Category" +
              $" @CategoryID = {entity.CategoryID}," +
              $" @UserID = {entity.UserId}," +
              $"@CategoryName = '{entity.@CategoryName}'"; 

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync(query);
                return result.ToString();
            }

        }

        public async Task<string> Delete(int id)
        {
            Category entity = await GetByID(id);

            var query = $"EXEC Delete_Category" +
              $" @CategoryID = {entity.CategoryID}";

            using (var connection = _context.CreateConnection())
            {
                var result = await connection.ExecuteScalarAsync(query);
                return result.ToString();
            }

            //context.Category.Remove(entity);
            //await context.SaveChangesAsync();
        }

    }
}
