using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IStatusRepository
    {
        Task<IEnumerable<Status>> GetAllOrderStatus();
    }
}
