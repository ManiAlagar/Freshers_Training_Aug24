using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Repository.Interface
{
    public interface IBudgetRepository
    {
       
        Task<IEnumerable<Budget>> Get(int id);

        Task<Budget> GetByID(int id);

        Task Add(Budget entity);

        Task Edit(Budget entity);

        Task Delete(int id);
    }
}
