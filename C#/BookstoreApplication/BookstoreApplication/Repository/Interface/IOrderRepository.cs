using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IOrderRepository
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> AddOrder(Order Order);
        Task<Order> GetOrderById(int Id);
        Task<Order> DeleteOrder(int id);
        Task<Order> UpdateOrder(int Id, Order Order);
    }
}
