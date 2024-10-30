

using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IStatusService
    {
        Task<IEnumerable<Status>> GetAllOrderStatus();
    }
}
