using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Net;
using System.Web.Http;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookstoreApplication.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        public readonly BookDBContext db;
        public readonly IHttpContextAccessor httpContextAccessor;
        private readonly DapperContext _context;

        public BookRepository(BookDBContext context, IHttpContextAccessor httpContextAccessor, DapperContext _context)
        {
            this.db = context;
            this.httpContextAccessor = httpContextAccessor;
            this._context = _context;
        }


        public async Task<IEnumerable<Book>> GetAllBooks()
        {
           
            var roleId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.microsoft.com/ws/2008/06/identity/claims/role").Value;
            var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            // return await db.Orders.ToListAsync();
            var query = $"select books.*,users.Username,case when cart.CartItemId>0 then 1 else 0 end as isAddedToCart from books Join Users on books.userid=users.userid left Join Cart on Cart.BookId = Books.BookId and cart.UserId={userId} and cart.status='Pending' ";
            
            using (var connection = _context.CreateConnection())
            {
                var books = await connection.QueryAsync<Book>(query);
                if (roleId == "2")//author
                {
                    return  books.Where(a => a.UserId == Convert.ToInt32(userId) && a.Stock>0).ToList();
                }
                else
                if (roleId == "1")//customers
                {
                    return  books.Where(a => a.IsPublish == true && a.Stock >0).ToList();
                }
                else//admin
                {
                    //return  books.Where(a => a.IsPublish == false ).ToList();
                    return books.ToList();
                }
                
            }
        }
        [Authorize(Roles =  "2")]
        public async Task<Book> AddBook(Book Book)
        {
            var userId = httpContextAccessor.HttpContext.User.Claims.SingleOrDefault(u => u.Type == "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier").Value;
            var newBook = new Book()
            {
                Title = Book.Title,
                UserId = Convert.ToInt32(userId),
                Description = Book.Description,
                Price = Book.Price,
                Stock = Book.Stock,
                PublishedDate = Book.PublishedDate,
                IsPublish = Book.IsPublish
            };
            await db.Books.AddAsync(newBook);
            await db.SaveChangesAsync();
            return newBook;
        }

        public async Task<Book> GetBookById(int bookId)
        {
            var book = await db.Books.FindAsync(bookId);
            return book;
        }

        public async Task<Book> DeleteBook(int id)
        {
            try
            {
                var Book = await db.Books.FindAsync(id);
                if (Book != null)
                {
                    db.Remove(Book);
                    await db.SaveChangesAsync();
                }
                return Book;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<Book> UpdateBook(int BookId, Book Book)
        {
            var existingBook = await db.Books.FindAsync(BookId);
            if (existingBook != null)
            {
                existingBook.Title = Book.Title;
                existingBook.Description = Book.Description;
                existingBook.Price = Book.Price;
                existingBook.Stock = Book.Stock;
                await db.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }
        //IsPublish

        public async Task<bool> IsPublish(int BookId)
        {
            var existingBook = await db.Books.FindAsync(BookId);
            if (existingBook != null)
            {
                using (var connection = _context.CreateConnection())
                {
                    var queryParameters = new DynamicParameters();
                    queryParameters.Add("@BookId", BookId);

                    try
                    {
                        var res = await connection.QueryAsync<int>(
                        "IsPublish",
                        queryParameters,
                        commandType: CommandType.StoredProcedure);
                        return true;
                    }
                    catch
                    {
                        throw new HttpResponseException(HttpStatusCode.BadRequest);
                    }

                }
            }
            return true;
        }

    }
}
