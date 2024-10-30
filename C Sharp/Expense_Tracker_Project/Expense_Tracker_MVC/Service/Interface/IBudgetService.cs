using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface IBudgetService
    {
        Task<IEnumerable<Budget>> Get();

        Task<Budget> GetByID(int? id);

        Task<string> Create(Budget entity);

        Task<string> Edit(Budget entity);

        Task<string> Delete(int id);
    }
}
