using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository.Implementation
{
    public class CartRepository: ICartRepository
    {
        public readonly BookDBContext db;
        public CartRepository(BookDBContext context)
        {
            db = context;
        }
        public async Task<Cart> AddItemToCart(Cart Cart)
        {
            var newItem = new Cart()
            {
                UserId = Cart.UserId,
                BookId = Cart.CartId,
                Quantity = Cart.Quantity,
                discount = Cart.discount,
                Shipping = Cart.Shipping
            };
            await db.Cart.AddAsync(newItem);
            await db.SaveChangesAsync();
            return newItem;
        }
        public async Task<Cart> DeleteItemFromCart(int id)
        {
            var CartItem = await db.Cart.FindAsync(id);
            if (CartItem != null)
            {
                db.Remove(CartItem);
                await db.SaveChangesAsync();

            }
            return CartItem;
        }
        public async Task<IEnumerable<Cart>> GetAllCartItems()
        {
            return await db.Cart.ToListAsync();
        }

        public async Task<Cart> GetCartById(int Id)
        {
            var cart = await db.Cart.FindAsync(Id);
            return cart;
        }
    }
}
