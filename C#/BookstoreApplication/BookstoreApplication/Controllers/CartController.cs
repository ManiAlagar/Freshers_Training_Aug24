using BookstoreApplication.Models;
using BookstoreApplication.Service.Implementation;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[Controller]")]
    public class CartController : Controller
    {
        private readonly ICartService _cartService;
        public CartController(ICartService _cartService)
        {
             this._cartService= _cartService;
        }

        [Authorize(Roles = "1")]
        [HttpGet("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems()
        {
            try
            {
                var res = Ok(new ApiResponse<IEnumerable<Cart>> ("Retreived successfully", 200, await _cartService.GetAllCartItems() ));
                return res;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
        [Authorize(Roles = "1")]
        [HttpPost("AddItemToCart")]
        public async Task<ActionResult<Cart>> AddItemToCart([FromQuery] int bookId)
        {
            try
            {
                if (bookId == null)
                    return BadRequest();

                var created = await _cartService.AddItemToCart(bookId);
                var res = new ApiResponse<Cart>("Added successfully", 200, created);
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }
        [Authorize(Roles = "1")]
        [HttpPost("UpdateQuantity")]
        public async Task<ActionResult<Cart>> UpdateQuantity([FromQuery] int cartItemId, int quantity)
        {
            try
            {
                if (quantity == null)
                    return BadRequest();

                var created = await _cartService.UpdateQuantity(cartItemId, quantity);
                var res = new ApiResponse<Cart>("Updated successfully", 200, created);
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }
        [Authorize(Roles = "1")]
        [HttpDelete]
        [Route("DeleteItemFromCart/{id:int}")]
        public async Task<ActionResult<Cart?>> DeleteItemFromCart([FromRoute] int id)
        {
            try
            {
                var Cart = await _cartService.GetCartItemById(id);

                if (Cart == null)
                {
                    return NotFound($"Item with Id = {id} not found");
                }
                var res= await _cartService.DeleteItemFromCart(id);
                var result = new ApiResponse<Cart>("Deleted successfully", 200, res);
                return Ok(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        [Authorize(Roles = "1,3")]
        [HttpGet]
        [Route("GetCartById/{id:int}")]
        public async Task<ActionResult<Cart?>> GetCartItemById([FromRoute] int id)

        {
            try
            {
                var result = await _cartService.GetCartItemById(id);

                if (result == null) return NotFound();

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize(Roles = "1")]
        [HttpGet]
        [Route("Discount")]
        public async Task<IActionResult> Discount()

        {
            try
            {
                var res = Ok(new ApiResponse<IEnumerable<Discount>>("Retreived successfully", 200, await _cartService.Discount()));


                return res;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }


    }
}
