using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Services.Interface
{
    public interface IExpenseService
    {
        Task<IEnumerable<Expenses>> Get(int id);
        
        Task<Expenses> GetByID(int id);

        Task Add(Expenses entity);

        Task Edit(Expenses entity);

        Task Delete(int id);
    }
}
