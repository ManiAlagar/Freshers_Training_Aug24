
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Repository.Interface;
using Expense_Tracker_API.Services.Interface;

namespace Expense_Tracker_API.Services.Implement
{
    public class CategorySpendService : ICategorySpendService
    {

        private readonly ICategorySpendRepository categorySpendRepository;



        public CategorySpendService(ICategorySpendRepository categorySpendRepository)
        {
            this.categorySpendRepository = categorySpendRepository;
        }


        public async Task<IEnumerable<CategorySpend>> Get(int id)
        {
            return await categorySpendRepository.Get(id);
        }


    }
}
