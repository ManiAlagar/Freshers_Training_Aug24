using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> Get();

        Task<Category> Get(int? id);

        Task<string> Create(Category entity);

        Task<string> Edit(Category entity);

        Task<string> Delete(int id);
    }
}
