
using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IBookService
    {
        Task<IEnumerable<Book>> GetAllBooks(string? token); 
       
        Task<int> AddBook(Book Book, string? token);
        Task<int> UpdateBook(int id, Book Book, string? token);
        Task<int> DeleteBook(int Id, string? token);
        Task<Book> GetBookById(int Id, string? token);
        Task IsPublish(int id, string token);
        //---

    }
}
