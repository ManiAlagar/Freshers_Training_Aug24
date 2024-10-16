using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllCartItems();
        Task<Cart> AddItemToCart(Cart Cart);
        Task<Cart> DeleteItemFromCart(int id);
        Task<Cart> GetCartById(int Id);
    }
}
