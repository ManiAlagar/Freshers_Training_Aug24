using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface ICategoryService
    {
        Task<IEnumerable<Category>> Get();

        Task<Category> Get(int? id);

        Task Create(Category entity);

        Task Edit(Category entity);

        Task Delete(int id);

    }
}
