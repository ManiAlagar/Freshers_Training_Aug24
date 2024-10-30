using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Repository.Implement;
using Expense_Tracker_API.Repository.Interface;
using Expense_Tracker_API.Services.Interface;

namespace Expense_Tracker_API.Services.Implement
{
    public class ConfigurationService : IConfigurationService
    { 
        private readonly IConfigurationRepository configurationRepository;
        public ConfigurationService(IConfigurationRepository configurationRepository)
        {
            this.configurationRepository = configurationRepository;
        }


        public async Task<IEnumerable<UserConfiguration>> Get(int id)
        {
            return await configurationRepository.Get(id);
        }

        public async Task<UserConfiguration> GetById(int id)
        {
            var entity = await configurationRepository.GetById(id);
            return entity;
        }


        public async Task Add(UserConfiguration entity)
        {      
            await configurationRepository.Add(entity);
        }

        public async Task Edit(UserConfiguration entity)
        {
            await configurationRepository.Edit(entity);
        }
    }
}
