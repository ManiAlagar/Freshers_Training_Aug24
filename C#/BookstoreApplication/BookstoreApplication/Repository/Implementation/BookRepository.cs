using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace BookstoreApplication.Repository.Implementation
{
    public class BookRepository : IBookRepository
    {
        public readonly BookDBContext db;

        public BookRepository(BookDBContext context)
        {
            db = context;
        }
        

        public async Task<IEnumerable<Book>> GetAllBooks()
        {
            return await db.Books.ToListAsync();
        }

        public async Task<Book> AddBook(Book Book)
        {
            var newBook = new Book()
            {
                Title = Book.Title,
                AuthorName=Book.AuthorName,
                Description=Book.Description,
                Price = Book.Price,
                Stock = Book.Stock,
                PublishedDate = Book.PublishedDate,
                IsPublish=Book.IsPublish
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
            var Book = await db.Books.FindAsync(id);
            if (Book != null)
            {
                db.Remove(Book);
                await db.SaveChangesAsync();

            }
            return Book;
        }

        public async Task<Book> UpdateBook(int BookId, Book Book)
        {
            var existingBook = await db.Books.FindAsync(BookId);
            if (existingBook != null)
            {
                existingBook.Title = Book.Title;
                existingBook.AuthorName = Book.AuthorName;
                existingBook.Description = Book.Description;
                existingBook.Price = Book.Price;
                existingBook.Stock = Book.Stock;
                existingBook.PublishedDate = Book.PublishedDate;
                existingBook.IsPublish = Book.IsPublish;
                await db.SaveChangesAsync();
                return existingBook;
            }
            return null;
        }
    }
}
