
using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks(string? token);
        Task AddBook(Book Book, string? token);
        Task UpdateBook(int id, Book Book, string? token);
        Task DeleteBook(int Id, string? token);
        Task<Book> GetBookById(int Id, string? token);
        
        
    }
}
