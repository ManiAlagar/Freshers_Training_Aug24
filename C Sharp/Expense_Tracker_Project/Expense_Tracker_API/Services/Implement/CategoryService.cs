
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Repository.Implement;
using Expense_Tracker_API.Repository.Interface;
using Expense_Tracker_API.Services.Interface;

namespace Expense_Tracker_API.Services.Implement
{
    public class CategoryService : ICategoryService
    {

        private readonly ICategoryRepository categoryRepository;

        public CategoryService(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        public async Task<IEnumerable<Category>> Get(int id)
        {
            return await categoryRepository.Get(id);
        }

        public async Task<Category> GetByID(int id)
        {
            return await categoryRepository.GetByID(id);
        }

        public async Task Add(Category entity)
        {
            await categoryRepository.Add(entity);
        }

        public async Task Edit(Category entity)
        {
            await categoryRepository.Edit(entity);
        }


        public async Task Delete(int id)
        {
            await categoryRepository.Delete(id);
        }
    }
}
