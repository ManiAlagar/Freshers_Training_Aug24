using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCartItems();
        Task<Cart> AddItemToCart(int bookId);
        Task<Cart> DeleteItemFromCart(int id);
        Task<Cart> GetCartItemById(int Id);
        Task<Cart> UpdateQuantity(int cartId, int quantity);
        Task<IEnumerable<Discount>> Discount();
    }
}
