
using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IOrderService
    {
        Task AddOrder(string address, string token);
        Task DeleteOrder(int id, string token);
        Task<IEnumerable<Order>> GetAllOrders(string? token);
        Task<IEnumerable<Order>> GetBooksFromOrder(int id, string? token);
        Task UpdateOrder(int orderId, int statusId, string token);

        
    }
}
