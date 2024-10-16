using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCartItems();
        Task<Cart> AddItemToCart(Cart Cart);
        Task<Cart> DeleteItemFromCart(int id);
        Task<Cart> GetCartById(int Id);
    }
}
