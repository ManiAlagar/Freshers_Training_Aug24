using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<Order> AddOrder(Order Order);
        Task<Order> GetOrderById(int Id);
        Task<Order> DeleteOrder(int id);
        Task<Order> UpdateOrder(int Id, Order Order);
    }
}
