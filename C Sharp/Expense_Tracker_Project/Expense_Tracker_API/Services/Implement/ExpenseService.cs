using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Repository.Implement;
using Expense_Tracker_API.Repository.Interface;
using Expense_Tracker_API.Services.Interface;

namespace Expense_Tracker_API.Services.Implement
{
    public class ExpenseService : IExpenseService
    {
        private readonly IExpenseRepository expenseRepository;
        public ExpenseService(IExpenseRepository expenseRepository)
        {
            this.expenseRepository = expenseRepository;
        }

        public async Task<IEnumerable<Expenses>> GetMonthly(int id)
        {
            return await expenseRepository.GetMonthly(id);
        }
           public async Task<IEnumerable<Expenses>> Get(int id)
        {
            return await expenseRepository.Get(id);
        }

        public async Task<Expenses> GetByID(int id)
        {
            return await expenseRepository.GetByID(id);
        }

        public async Task Delete(int id)
        {
            await expenseRepository.Delete(id);
        }

        public async  Task<string> Add(Expenses entity)
        {
            return await expenseRepository.Add(entity);
        }

        public async Task Edit(Expenses entity)
        {
            await expenseRepository.Edit(entity);
        }

    }
}
