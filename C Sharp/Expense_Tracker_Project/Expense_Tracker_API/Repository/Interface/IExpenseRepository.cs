using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Repository.Interface
{
    public interface IExpenseRepository
    {
        Task<IEnumerable<Expenses>> Get(int id);
        Task<IEnumerable<Expenses>> GetMonthly(int id);

        Task<Expenses> GetByID(int id);

        Task Delete(int id);
        Task<string> Add(Expenses entity);
        Task Edit(Expenses entity);
    }
}
