
using BookstoreApplication.Models;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Service.Interface
{
    public interface IBookService
    {
        
        Task<IEnumerable<Book>> GetAllBooks();
        Task<Book> AddBook(Book book);
        Task<Book> GetBookById(int bookId);
        Task<Book> DeleteBook(int id);
        Task<Book> UpdateBook(int bookId, Book book);
        Task<bool> IsPublish(int bookId);
        
    }
}
 