using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Repository.Interface
{
    public interface ICategorySpendRepository
    {
        Task<IEnumerable<CategorySpend>> Get(int id);
    }
}
