using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IConfigService
    {
        Task<IEnumerable<Config>> GetAllConfigValues();
    }
}
