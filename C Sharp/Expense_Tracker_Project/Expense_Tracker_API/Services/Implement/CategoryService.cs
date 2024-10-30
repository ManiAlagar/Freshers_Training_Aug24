
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

        //public async Task<IEnumerable<CategorySpend>> GetCategorySpend(int id)
        //{
        //    return await categoryRepository.GetCategorySpend(id);
        //}

        public async Task<IEnumerable<Category>> Get(int id)
        {
            return await categoryRepository.Get(id);
        }

        public async Task<Category> GetByID(int id)
        {
            return await categoryRepository.GetByID(id);
        }

        public async Task<string> Add(Category entity)
        {
           return  await categoryRepository.Add(entity);
        }

        public async Task<string> Edit(Category entity)
        {
            return await categoryRepository.Edit(entity);
        }


        public async Task<string> Delete(int id)
        {
            return await categoryRepository.Delete(id);
        }
    }
}
