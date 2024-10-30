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

        public async Task<string> Add(Budget entity)
        {
            return await budgetRepository.Add(entity);
        }

        public async Task<string> Edit(Budget entity)
        {
           return  await budgetRepository.Edit(entity);
        }

        public async Task<string> Delete(int id)
        {
           return  await budgetRepository.Delete(id);
        }


      
    }
}
