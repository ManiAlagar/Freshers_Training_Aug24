using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllOrderStatus();
    }
}
