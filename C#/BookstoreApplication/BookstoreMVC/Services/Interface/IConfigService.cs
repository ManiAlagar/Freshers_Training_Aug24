using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IConfigService
    {
        Task<IEnumerable<Config>> GetAllConfigValues(string? token);
        Task<Config> GetConfigById(int Id, string? token);
        Task<int> UpdateConfig(int id, Config config, string? token);
    }
}
