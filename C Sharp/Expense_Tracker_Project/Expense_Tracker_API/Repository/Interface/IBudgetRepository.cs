using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Repository.Interface
{
    public interface IBudgetRepository
    {
        Task<IEnumerable<Budget>> Get(int id);

        Task<Budget> GetByID(int id);

        Task<string> Add(Budget entity);

        Task<string> Edit(Budget entity);

        Task<string> Delete(int id);
    }
}
