using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface ICategorySpendService
    {
        Task<IEnumerable<CategorySpend>> Get();
    }
}
