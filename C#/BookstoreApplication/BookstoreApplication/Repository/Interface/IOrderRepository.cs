using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<int> AddOrder(string Address);
        Task<Order> GetOrderById(int Id);
        Task<int> DeleteOrder(int id);
        Task<int> UpdateOrder(int OrderId, int StatusId);
        Task<IEnumerable<Order>> GetBooksFromOrder(int id);
    }
}
