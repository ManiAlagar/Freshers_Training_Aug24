using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IBookRepository
    {
        Task<IEnumerable<Book>> GetAllBooks();
        Task<int> AddBook(Book book);
        Task<Book> GetBookById(int bookId);
        Task<int> DeleteBook(int id);
        Task<int> UpdateBook(int bookId, Book book);
        Task<bool> IsPublish(int bookId);
        Task<IEnumerable<Book>> Books();
    }
}
