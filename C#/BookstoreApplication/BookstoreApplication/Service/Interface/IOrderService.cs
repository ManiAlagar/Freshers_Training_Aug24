using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Service.Interface
{
    public interface IOrderService
    {
        Task<IEnumerable<Order>> GetAllOrders();
        Task<int> AddOrder(string Address);
        Task<Order> GetOrderById(int Id);
        Task<int> DeleteOrder(int id);
        Task<int> UpdateOrder(int OrderId, int StatusId);
        Task<IEnumerable<Order>> GetBooksFromOrder(int id);
       
    }
}
