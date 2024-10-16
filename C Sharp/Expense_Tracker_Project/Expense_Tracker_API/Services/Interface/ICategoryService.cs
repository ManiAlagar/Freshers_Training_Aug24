using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Services.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> Get(int id);

        Task<Category> GetByID(int id);

        Task Add(Category entity);

        Task Edit(Category entity);

        Task Delete(int id);
    }
}
