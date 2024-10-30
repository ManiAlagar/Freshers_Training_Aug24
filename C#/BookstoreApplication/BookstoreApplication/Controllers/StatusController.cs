using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : Controller
    {
        private readonly IStatusService Service;
        public StatusController(IStatusService service)
        {
            this.Service = service;
        }
        [HttpGet("GetAllOrderStatus")]
        public async Task<IActionResult> GetAllOrderStatus()
        {
            try
            {
                return Ok(await Service.GetAllOrderStatus());
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                 "Error retrieving data from the database");
            }
        }
    }
}
