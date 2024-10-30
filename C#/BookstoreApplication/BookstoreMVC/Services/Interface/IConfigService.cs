using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IConfigService
    {
        Task<IEnumerable<Config>> GetAllConfigValues(string? token);
    }
}
