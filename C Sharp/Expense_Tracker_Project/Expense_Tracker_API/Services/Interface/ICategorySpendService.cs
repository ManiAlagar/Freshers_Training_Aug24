using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Services.Interface
{
    public interface ICategorySpendService
    {
        Task<IEnumerable<CategorySpend>> Get(int id);
    }
}
