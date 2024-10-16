using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_API.Repository.Implement
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly UserContext context;
        public CategoryRepository(UserContext context)
        {
            this.context = context;
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

        public async Task Add(Category entity)
        {
            await context.Category.AddAsync(entity);
            await context.SaveChangesAsync();
        }

        public async Task Edit(Category entity)
        {
            Category category = await GetByID(entity.CategoryID);

            category.CategoryName = entity.CategoryName;

            context.Category.Update(category);
            

            await  context.SaveChangesAsync();
            
        }

        public async Task Delete(int id)
        {
            Category entity = await GetByID(id);

            context.Category.Remove(entity);
            await context.SaveChangesAsync();
        }

    }
}
