using BookstoreApplication.Models;
using BookstoreApplication.Repository.Implementation;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Service.Implementation
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepository orderRepository;
        public OrderService(IOrderRepository orderRepository)
        {
            this.orderRepository = orderRepository;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await orderRepository.GetAllOrders();
        }
        public async Task<int> AddOrder(string Address)
        {
            return await orderRepository.AddOrder( Address);
        }
        public async Task<Order> GetOrderById(int Id)
        {
            return await orderRepository.GetOrderById(Id);
        }
        public async Task<int> UpdateOrder(int OrderId, int StatusId)
        {
            return await orderRepository.UpdateOrder(OrderId, StatusId);
        }
        public async Task<int> DeleteOrder(int id)
        {
            return await orderRepository.DeleteOrder(id);
        }
        public async Task<IEnumerable<Order>> GetBooksFromOrder(int id)
        {
            return await orderRepository.GetBooksFromOrder(id);
        }

        
    }
}
