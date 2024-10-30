using BookstoreApplication.Common;
using BookstoreApplication.Models;
using BookstoreApplication.Service.Implementation;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata;
using Constant = BookstoreApplication.Common.Constant;

namespace BookstoreApplication.Controllers
{
    [Authorize]
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
                return Ok(new ApiResponse<IEnumerable<Book>>("message",200,await BookService.GetAllBooks()));
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
        [Authorize(Roles="2,3")]
        [HttpPost("AddBook")]
        public async Task<ActionResult<Book>> AddBook(Book Book)
        {
            try
            {
                if (Book == null)
                    return BadRequest();

                var created = await BookService.AddBook(Book);
                var res = new ApiResponse<Book>("Created successfully", 200,created);
                return Ok(res);
                 
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }
        [Authorize(Roles = "2,3")]
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

        [Authorize(Roles = "2,3")]
        [HttpDelete]
        [Route("DeleteBook/{id:int}")]
        public async Task<ActionResult<Book?>> DeleteBook([FromRoute] int id)
        {
            try
            {
                var Book = await BookService.GetBookById(id);

                if (Book == null)
                {
                    return NotFound($"Book with Id = {id} not found");
                }
                var deleted= await BookService.DeleteBook(id);
                var res = new ApiResponse<Book>("Deleted successfully", 200, deleted);
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        [Authorize(Roles = "2,3")]
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
                    return NotFound($"Book with Id = {id} not found");

                var updated =await BookService.UpdateBook(id, Book);
                var res = new ApiResponse<Book>("Updated successfully", 200, updated);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


        [Authorize(Roles = "3")]
        [HttpPost("IsPublish")]
        public async Task<ActionResult<bool>> IsPublish([FromQuery] int bookId)
        {
            try
            {
                if (bookId == null)
                    return BadRequest();

                await BookService.IsPublish(bookId);
                return true;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }



    }
}
