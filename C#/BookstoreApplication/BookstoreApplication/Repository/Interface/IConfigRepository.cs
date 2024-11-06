using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IConfigRepository
    {
        Task<IEnumerable<Config>> GetAllConfigValues();
        Task<Config> GetConfigById(int Id);
        Task<int> UpdateConfig(int Id, Config config);
    }
}
