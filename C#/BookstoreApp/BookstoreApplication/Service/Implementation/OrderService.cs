using BookstoreApplication.Models;
using BookstoreApplication.Repository.Implementation;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class OrderService:IOrderService
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
        public async Task<Order> AddOrder(Order Order)
        {
            return await orderRepository.AddOrder(Order);
        }
        public async Task<Order> GetOrderById(int Id)
        {
            return await orderRepository.GetOrderById(Id);
        }
        public async Task<Order> UpdateOrder(int Id, Order Order)
        {
            return await orderRepository.UpdateOrder(Id, Order);
        }
        public async Task<Order> DeleteOrder(int id)
        {
            return await orderRepository.DeleteOrder(id);
        }
    }
}
