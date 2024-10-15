using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository.Implementation
{
    public class OrderRepository: IOrderRepository
    {
        private readonly BookDBContext db;
        public OrderRepository(BookDBContext _dbContext)
        {
            this.db = _dbContext;
        }
        public async Task<IEnumerable<Order>> GetAllOrders()
        {
            return await db.Orders.ToListAsync();
        }

        public async Task<Order> AddOrder(Order Order)
        {
            var newOrder = new Order()
            {
                UserId = Order.UserId,
                BookId = Order.BookId,
                Quantity = Order.Quantity,
                discount = Order.discount,
                Shipping = Order.Shipping,
                TotalAmount = Order.TotalAmount,
                Ordered_Date=Order.Ordered_Date,
                StatusId=Order.StatusId
            };
            await db.Orders.AddAsync(newOrder);
            await db.SaveChangesAsync();
            return newOrder;
        }

        public async Task<Order> GetOrderById(int Id)
        {
            var Order = await db.Orders.FindAsync(Id);
            return Order;
        }

        public async Task<Order> DeleteOrder(int id)
        {
            var Order = await db.Orders.FindAsync(id);
            if (Order != null)
            {
                db.Remove(Order);
                await db.SaveChangesAsync();

            }
            return Order;
        }

        public async Task<Order> UpdateOrder(int Id, Order Order)
        {
            var existingOrder = await db.Orders.FindAsync(Id);
            if (existingOrder != null)
            {
                existingOrder.UserId = Order.UserId;
                existingOrder.BookId = Order.BookId;
                existingOrder.Quantity = Order.Quantity;
                existingOrder.discount = Order.discount;
                existingOrder.Shipping = Order.Shipping;
                existingOrder.TotalAmount = Order.TotalAmount;
                existingOrder.Ordered_Date = Order.Ordered_Date;
                existingOrder.StatusId = Order.StatusId;
                await db.SaveChangesAsync();
                return existingOrder;
            }
            return null;
        }
    }
}
