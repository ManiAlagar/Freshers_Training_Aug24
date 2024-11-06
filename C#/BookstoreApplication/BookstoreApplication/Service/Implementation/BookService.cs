using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class BookService : IBookService
    {
        private readonly IBookRepository BookRepository;
        public BookService(IBookRepository BookRepository)
        {
            this.BookRepository = BookRepository;
        }
        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await BookRepository.GetAllBooks();
        }

        public async Task<IEnumerable<Book>> Books()
        {
            return await BookRepository.Books();
        }
        public async Task<int> AddBook(Book Book)
        {
            return await BookRepository.AddBook(Book);
        }
        public async Task<Book> GetBookById(int BookId)
        {
            return await BookRepository.GetBookById(BookId);
        }
        public async Task<int> UpdateBook(int BookId, Book Book)
        {
            return await BookRepository.UpdateBook(BookId, Book);
        }
        public async Task<int> DeleteBook(int id)
        {
            return await BookRepository.DeleteBook(id);
        }

        public async Task<bool> IsPublish(int BookId)
        {
            return await BookRepository.IsPublish(BookId);
        }


    }
}
