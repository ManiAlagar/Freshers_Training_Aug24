 using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNet.SignalR.Hubs;
using Microsoft.AspNetCore.Mvc;

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
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized");
            }
            var books = await _service.GetAllBooks(token);
            return View(books);
        }
        [HttpGet]
        public async Task<IActionResult> Publish()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return View("Unauthorized");
            }
            var books = await _service.GetAllBooks(token);
            return View(books);
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
                return View("Unauthorized");
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
                return View("Unauthorized");
            }
            await _service.AddBook(book, token);

            return RedirectToAction("Index", "Book");
        }

        [HttpGet]
        [Route("Student/Display/{id}")]
        public async Task<IActionResult> Display(int id)
        {
            var token = HttpContext.Session.GetString("token");
            TempData["view"] = "update";
            if (token == null)
            {
                return View("Unauthorized");
            }
            var book = await _service.GetBookById(id, token);
            if (book == null)
            {
                TempData["failure"] = "Error occurred";
            }
            return View(book);
        }

    }
}
