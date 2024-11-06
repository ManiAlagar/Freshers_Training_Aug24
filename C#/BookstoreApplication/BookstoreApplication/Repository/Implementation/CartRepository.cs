using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Web.Http;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookstoreApplication.Repository.Implementation
{
    public class CartRepository: ICartRepository
    {
        public readonly BookDBContext db;
        public readonly IHttpContextAccessor httpContextAccessor;
        private readonly DapperContext _context;
        public CartRepository(BookDBContext context,IHttpContextAccessor httpContextAccessor, DapperContext _context)
        {
            this.db = context;
            this.httpContextAccessor = httpContextAccessor;
            this._context = _context;
        }
        public async Task<Cart> AddItemToCart(int bookId)
        {
            try
            {
                var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                var name = "SELECT title from books where BookId=@bookId";
                var stock = "SELECT Stock from books where BookId=@bookId";
                var amt = "SELECT price from books where BookId=@bookId";
                // var res = $"if exists(select CartItemId from cart where bookId=@bookId and status='Pending' AND userid={userId}) begin select 1 end else begin select 2 end";
                
                using (var connection = _context.CreateConnection())
                {

                    var title = await connection.QuerySingleOrDefaultAsync<string>(name, new { bookId });
                    var stocks = await connection.QuerySingleOrDefaultAsync<int>(stock, new { bookId });
                    var amount = await connection.QuerySingleOrDefaultAsync<decimal>(amt, new { bookId });
                   

                    if ( stocks > 0)
                    {
                        var item = new Cart()
                        {
                            UserId = Convert.ToInt32(userId),
                            BookId = bookId,
                            Quantity = 1,
                            Status = "Pending",
                            Stock = stocks,
                            Title = title,
                            Price = amount
                        };
                        await db.Cart.AddAsync(item);
                        await db.SaveChangesAsync();
                        return item;
                    }
                    return null;
                }
            }
            catch (Exception)
            {

                throw;
            }
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
        public async Task<IEnumerable<Cart>> GetAllCartItems()//Role
        {

            try
            {
                var roleId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
                var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                if(roleId=="1" || roleId=="2")//customer
                {
                    using (var connection = _context.CreateConnection())
                    {
                        var queryParameters = new DynamicParameters();
                        queryParameters.Add("@UserId", userId);

                        try
                        {
                            var res = await connection.QueryAsync<Cart>(
                            "GetCartDetails",
                            queryParameters,
                            commandType: CommandType.StoredProcedure);
                            return res;
                        }
                        catch
                        {
                            throw new HttpResponseException(HttpStatusCode.BadRequest);
                        }
                    }
                }
                else
                {
                    var cartItems=await db.Cart.ToListAsync();
                    return cartItems;
                }

            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Cart> GetCartItemById(int Id)
        {
            var cart = await db.Cart.FindAsync(Id);
            return cart;
        }

        public async Task<int> UpdateQuantity(int cartId, int quantity)
        {
            var price = "select BOOKS.price from books join cart on books.BookId=cart.bookid where cart.CartItemId=@cartId;";
            var query = "select stock from books join cart on cart.BookId = Books.BookId where cart.CartItemId=@cartId";

            using (var connection = _context.CreateConnection())
            {

                var BookPrice = await connection.QuerySingleOrDefaultAsync<decimal>(price, new { cartId });
                var stock= await connection.QuerySingleOrDefaultAsync<int>(query, new { cartId });
                try
                {
                    var cart = await db.Cart.FindAsync(cartId);
                    if (cart != null && quantity > 0 && quantity <=stock)
                    {
                        cart.Quantity = quantity;
                        cart.Price = BookPrice * quantity;
                        await db.SaveChangesAsync();
                        return 1;
                    }
                    return stock;
                }
                catch (Exception)
                {

                    throw;
                }
            }
            
        }

        public async Task<IEnumerable<Discount>> Discount()
        {
            try
            {
                var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
                using (var connection = _context.CreateConnection())
                {
                    var queryParameters = new DynamicParameters();
                    queryParameters.Add("@UserId", userId);

                    try
                    {
                        var res = await connection.QueryAsync<Discount>(
                        "discount",
                        queryParameters,
                        commandType: CommandType.StoredProcedure);
                        return res;
                    }
                    catch
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
