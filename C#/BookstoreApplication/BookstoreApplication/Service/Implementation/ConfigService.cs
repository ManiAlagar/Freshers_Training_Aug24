using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class ConfigService : IConfigService
    {
        private readonly IConfigRepository _configRepository;
        public ConfigService(IConfigRepository _configRepository)
        {
            this._configRepository = _configRepository;
        }
        public async Task<IEnumerable<Config>> GetAllConfigValues()
        {
            return await _configRepository.GetAllConfigValues();
        }
    }
}
