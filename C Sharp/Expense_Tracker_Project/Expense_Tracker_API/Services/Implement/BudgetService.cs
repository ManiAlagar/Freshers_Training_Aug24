using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Repository.Implement;
using Expense_Tracker_API.Repository.Interface;
using Expense_Tracker_API.Services.Interface;

namespace Expense_Tracker_API.Services.Implement
{
    public class BudgetService : IBudgetService
    {
        private readonly IBudgetRepository budgetRepository;
        public BudgetService(IBudgetRepository budgetRepository)
        {
            this.budgetRepository = budgetRepository;
        }

        public async Task<IEnumerable<Budget>> Get(int id)
        {
            return await budgetRepository.Get(id);
        }

        public async Task<Budget> GetByID(int id)
        {
            return await budgetRepository.GetByID(id);
        }

        public async Task Add(Budget entity)
        {
            await budgetRepository.Add(entity);
        }

        public async Task Edit(Budget entity)
        {
            await budgetRepository.Edit(entity);
        }

        public async Task Delete(int id)
        {
            await budgetRepository.Delete(id);
        }


      
    }
}
