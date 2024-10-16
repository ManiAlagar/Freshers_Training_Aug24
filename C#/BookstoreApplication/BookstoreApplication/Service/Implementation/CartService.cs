using BookstoreApplication.Models;
using BookstoreApplication.Repository.Implementation;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class CartService:ICartService
    {
        private readonly ICartRepository _cartRepository;
        public CartService(ICartRepository _cartRepository)
        {
            this._cartRepository = _cartRepository;
        }

        public async Task<Cart> AddItemToCart(Cart Cart)
        {
            return await _cartRepository.AddItemToCart(Cart);
        }

        public async Task<Cart> DeleteItemFromCart(int id)
        {
            return await _cartRepository.DeleteItemFromCart(id);
        }

        public async Task<IEnumerable<Cart>> GetAllCartItems()
        {
            return await _cartRepository.GetAllCartItems();
        }

        public async Task<Cart> GetCartById(int Id)
        {
            return await _cartRepository.GetCartById(Id);
        }
    }
}
