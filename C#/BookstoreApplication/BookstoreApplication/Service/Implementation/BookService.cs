﻿using BookstoreApplication.Models;
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
        public async Task<Book> AddBook(Book Book)
        {
            return await BookRepository.AddBook(Book);
        }
        public async Task<Book> GetBookById(int BookId)
        {
            return await BookRepository.GetBookById(BookId);
        }
        public async Task<Book> UpdateBook(int BookId, Book Book)
        {
            return await BookRepository.UpdateBook(BookId, Book);
        }
        public async Task<Book> DeleteBook(int id)
        {
            return await BookRepository.DeleteBook(id);
        }


    }
}