using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> Get();

        Task<Budget> GetByID(int? id);

        Task Delete(int id);

        Task Create(Budget entity);

        Task Edit(Budget entity);
    }
}
