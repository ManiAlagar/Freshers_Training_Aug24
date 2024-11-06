using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface ICartRepository
    {
        Task<IEnumerable<Cart>> GetAllCartItems();
        Task<Cart> AddItemToCart(int bookId);
        Task<Cart> DeleteItemFromCart(int id);
        Task<Cart> GetCartItemById(int Id);
        Task<int> UpdateQuantity(int cartItemId, int quantity);

        Task<IEnumerable<Discount>> Discount();
    }
}
