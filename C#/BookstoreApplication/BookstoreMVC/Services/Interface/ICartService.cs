
using Cart = BookstoreMVC.Models.Cart;
using Discount = BookstoreMVC.Models.Discount;


namespace BookstoreMVC.Services.Interface
{
    public interface ICartService
    {
        Task<IEnumerable<Cart>> GetAllCartItems(string? token);
        Task AddItemToCart(int id, string token);
        Task DeleteItemFromCart(int id, string? token);
        Task<Cart> GetCartItemById(int Id, string? token);
        Task<int> UpdateQuantity(int id, int quantity,string token);
        Task<IEnumerable<Discount>> Discount(string? token);
    }
}
