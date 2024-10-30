using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Repository.Interface
{
    public interface ICategoryRepository
    {
        Task<IEnumerable<Category>> Get(int id);

        Task<Category> GetByID(int id);

        Task<string> Add(Category entity);

        Task<string> Edit(Category entity);
      
        Task<string> Delete(int id);
    }
}
