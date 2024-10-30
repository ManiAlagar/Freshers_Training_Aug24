using BookstoreApplication.Models;
using BookstoreApplication.Service.Implementation;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        [Authorize(Roles = "1,3")]
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                var res = Ok(new ApiResponse<IEnumerable<Order>>("Retreived successfully", 200, await orderService.GetAllOrders()));
                return res;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [Authorize(Roles = "1,3")]
        [HttpPost("AddOrder")]
        public async Task<ActionResult<int>> AddOrder(string Address)
        {
            try
            {
                if (Address == null)
                    return BadRequest();

                var created = await orderService.AddOrder(Address);
                var res = new ApiResponse<int>("Added successfully", 200, created);
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new order record");
            }
        }
        [Authorize(Roles = "1,3")]
        [HttpGet]
        [Route("GetOrderById/{id:int}")]
        public async Task<ActionResult<Order?>> GetOrderById([FromRoute] int id)

        {
            try
            {
                var result = await orderService.GetOrderById(id);

                if (result == null) return NotFound(); 

                return result;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }
        [Authorize(Roles = "1,3")]
        [HttpGet("GetBooksFromOrder/{id:int}")]  
        public async Task<IActionResult> GetBooksFromOrder([FromRoute] int id)

        {
            try
            {
                var result = await orderService.GetBooksFromOrder(id);

                if (result == null) return NotFound();

                var res = new ApiResponse<IEnumerable<Order>>("success", 200, result);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error retrieving data from the database");
            }
        }

        [Authorize(Roles = "1,3")]
        [HttpDelete]
        [Route("DeleteOrder/{id:int}")]
        public async Task<ActionResult<int>> DeleteOrder([FromRoute] int id)
        {
            try
            {
                var Order = await orderService.GetOrderById(id);

                if (Order == null)
                {
                    return NotFound($"Item with Id = {id} not found");
                }
                var deleted =await orderService.DeleteOrder(id);
                var res = new ApiResponse<int>("Deleted successfully", 200, deleted);
                return Ok(res);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }
        
        [HttpPost("UpdateOrder")]
        public async Task<ActionResult<int?>> UpdateOrder([FromQuery] int orderId, int statusId)
        {
            try
            {
                if (orderId == null)
                    return BadRequest("order ID mismatch");


                
                var res = await orderService.UpdateOrder(orderId, statusId);
                return res;
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
