using Expense_Tracker_API.Entity;

namespace Expense_Tracker_API.Services.Interface
{
    public interface IConfigurationService
    {
   
        Task<IEnumerable<UserConfiguration>> Get(int id);

        Task<UserConfiguration> GetById(int id);

        Task Add(UserConfiguration entity);
        
        Task Edit(UserConfiguration entity);

    }
}
