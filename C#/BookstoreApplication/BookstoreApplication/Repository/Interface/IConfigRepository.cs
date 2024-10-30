using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IConfigRepository
    {
        Task<IEnumerable<Config>> GetAllConfigValues();
    }
}
