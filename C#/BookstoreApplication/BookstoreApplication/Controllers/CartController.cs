using BookstoreApplication.Models;
using BookstoreApplication.Service.Implementation;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService _cartService)
        {
             this._cartService= _cartService;
        }

        [HttpGet("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                return Ok(await _cartService.GetAllCartItems());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPost("AddItemToCart")]
        public async Task<ActionResult<Cart>> AddItemToCart(Cart Cart)
        {
            try
            {
                if (Cart == null)
                    return BadRequest();

                var created = await _cartService.AddItemToCart(Cart);
                return created;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }

        [HttpDelete]
        [Route("DeleteItemFromCart/{id:int}")]
        public async Task<ActionResult<Cart?>> DeleteItemFromCart([FromRoute] int id)
        {
            try
            {
                var Cart = await _cartService.GetCartById(id);

                if (Cart == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }
                return await _cartService.DeleteItemFromCart(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpGet]
        [Route("GetCartById/{id:int}")]
        public async Task<ActionResult<Cart?>> GetCartById([FromRoute] int id)

        {
            try
            {
                var result = await _cartService.GetCartById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
    }
}
