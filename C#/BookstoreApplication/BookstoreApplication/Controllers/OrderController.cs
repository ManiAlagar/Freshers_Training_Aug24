using BookstoreApplication.Models;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : Controller
    {
        private readonly IOrderService orderService;
        public OrderController(IOrderService orderService)
        {
            this.orderService = orderService;
        }
        
        [HttpGet("GetAllOrders")]
        public async Task<IActionResult> GetAllOrders()
        {
            try
            {
                return Ok(await orderService.GetAllOrders());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }

        [HttpPost("AddOrder")]
        public async Task<ActionResult<Order>> AddOrder(Order Order)
        {
            try
            {
                if (Order == null)
                    return BadRequest();

                var created = await orderService.AddOrder(Order);
                return created;

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                "Error creating new user record");
            }
        }

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


        [HttpDelete]
        [Route("DeleteOrder/{id:int}")]
        public async Task<ActionResult<Order?>> DeleteOrder([FromRoute] int id)
        {
            try
            {
                var Order = await orderService.GetOrderById(id);

                if (Order == null)
                {
                    return NotFound($"Student with Id = {id} not found");
                }
                return await orderService.DeleteOrder(id);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error deleting data");
            }
        }

        [HttpPut]
        [Route("UpdateOrder/{id:int}")]
        public async Task<ActionResult<Order?>> UpdateOrder([FromRoute] int id, Order Order)
        {
            try
            {
                if (id == null)
                    return BadRequest("Book ID mismatch");

                var OrderToUpdate = await orderService.GetOrderById(id);

                if (OrderToUpdate == null)
                    return NotFound($"Student with Id = {id} not found");

                return await orderService.UpdateOrder(id, Order);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }
    }
}
