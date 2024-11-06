using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IConfigService
    {
        Task<IEnumerable<Config>> GetAllConfigValues();
        Task<Config> GetConfigById(int Id);
        Task<int> UpdateConfig(int Id, Config config);
    }
}
