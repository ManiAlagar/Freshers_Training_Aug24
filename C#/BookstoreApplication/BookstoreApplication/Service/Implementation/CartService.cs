using BookstoreApplication.Models;
using BookstoreApplication.Repository.Implementation;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace BookstoreApplication.Service.Implementation
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository _cartRepository)
        {
            this._cartRepository = _cartRepository;
        }

        public async Task<Cart> AddItemToCart(int bookId)
        {
            return await _cartRepository.AddItemToCart(bookId);
        }

        public async Task<Cart> DeleteItemFromCart(int id)
        {
            return await _cartRepository.DeleteItemFromCart(id);
        }

        public async Task<IEnumerable<Cart>> GetAllCartItems()
        {
            return await _cartRepository.GetAllCartItems();
        }

        public async Task<Cart> GetCartItemById(int Id)
        {
            return await _cartRepository.GetCartItemById(Id);
        }

        public async Task<int> UpdateQuantity(int cartId, int quantity)
        {
            return await _cartRepository.UpdateQuantity(cartId, quantity);
        }
        public async Task<IEnumerable<Discount>> Discount()
        {
            return await _cartRepository.Discount();
        }
    }
}
