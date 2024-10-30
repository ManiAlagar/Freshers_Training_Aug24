﻿using BookstoreApplication.Models;
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;
using Book = BookstoreMVC.Models.Book;

namespace BookstoreMVC.Controllers
{
    public class BookController : Controller
    {
        private readonly IBookService _service;
        public BookController(IBookService service)
        {
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            try
            {
                var token = HttpContext.Session.GetString("token");
                var role = HttpContext.Session.GetString("roleId");
                TempData["role"] = role;
                if (token == null)
                {
                    return View("Unauthorized", "Shared");
                }
                var books = await _service.GetAllBooks(token);
                return View(books);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [HttpPost, ActionName("DeleteBook")] 
        public async Task<bool> DeleteBook(int id)
        {

            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.DeleteBook(id, token);
            return true;
        }



        


        [HttpGet]
        public IActionResult Display()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Display([Bind] Book book)
        {
            
            TempData["success"] = "Created successfully";
            TempData["view"] = "create";
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            await _service.AddBook(book, token);

            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        [Route("Cart/Display/{id}")]
        public async Task<IActionResult> Display(int id)
        {
            var token = HttpContext.Session.GetString("token");
            TempData["view"] = "update";
            if (token == null)
            {
                return View("Unauthorized", "Shared");
            }
            var book = await _service.GetBookById(id, token);
            if (book == null)
            {
                TempData["failure"] = "Error occurred";
            }
            return View(book);
        }


        [HttpPost]
        [Route("Cart/Display/{id:int}")]
        public async Task<IActionResult> Display(int id, [Bind] Book book)
        {
            if (book.BookId > 0)
            {
                TempData["success"] = "Updated successfully";
                TempData["view"] = "update";
                var token = HttpContext.Session.GetString("token");
                if (token == null)
                {
                    return View("Unauthorized", "Shared");
                }
                await _service.UpdateBook(id, book, token);

            }
            else
            {
                var token = HttpContext.Session.GetString("token");
                if (token == null)
                {
                    return View("Unauthorized", "Shared");
                }
                await _service.AddBook(book, token);
            }
            return RedirectToAction("Index", "Book");

        }

        [HttpPost, ActionName("IsPublish")]
        public async Task<bool> IsPublish([FromQuery] string bookId)
        {

            var token = HttpContext.Session.GetString("token") ?? throw new NotAuthorizedException("Bearer");
            await _service.IsPublish(Convert.ToInt32(bookId), token);
            return true;
        }
    }
}
