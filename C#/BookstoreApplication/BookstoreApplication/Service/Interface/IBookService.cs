
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Service.Interface
{
    public interface IBookService
    {
        
        Task<IEnumerable<Book>> GetAllBooks();
        Task<IEnumerable<Book>> Books();
        Task<int> AddBook(Book book);
        Task<Book> GetBookById(int bookId);
        Task<int> DeleteBook(int id);
        Task<int> UpdateBook(int bookId, Book book);
        Task<bool> IsPublish(int bookId);
        
    }
}
 