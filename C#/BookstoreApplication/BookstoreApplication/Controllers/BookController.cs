using BookstoreApplication.Common;
using BookstoreApplication.Models;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Constant = BookstoreApplication.Common.Constant;

namespace BookstoreApplication.Controllers
{
    [Authorize(Roles="3")]
    [ApiController]
    [Route("api/[Controller]")]
    public class BookController : Controller
    {
        private readonly IBookService BookService;

        public BookController(IBookService BookService)
        {
            this.BookService = BookService;
        }
        

        [HttpGet("GetAllBooks")]
        public async Task<IActionResult> GetAllBooks()
        {
            try
            {
                return Ok(await BookService.GetAllBooks());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPost("AddBook")]
        public async Task<ActionResult<Book>> AddBook(Book Book)
        {
            try
            {
                if (Book == null)
                    return BadRequest();

                var created = await BookService.AddBook(Book);
                return created;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }

        [HttpGet]
        [Route("GetBookById/{id:int}")]
        public async Task<ActionResult<Book?>> GetBookById([FromRoute] int id)

        {
            try
            {
                var result = await BookService.GetBookById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


        [HttpDelete]
        [Route("DeleteBook/{id:int}")]
        public async Task<ActionResult<Book?>> DeleteBook([FromRoute] int id)
        {
            try
            {
                var Book = await BookService.GetBookById(id);

                if (Book == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }
                return await BookService.DeleteBook(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpPut]
        [Route("UpdateBook/{id:int}")]
        public async Task<ActionResult<Book?>> UpdateBook([FromRoute] int id, Book Book)
        {
            try
            {
                if (id == null)
                    return BadRequest("Book ID mismatch");

                var BookToUpdate = await BookService.GetBookById(id);

                if (BookToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");

                return await BookService.UpdateBook(id, Book);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }

        
    }
}
