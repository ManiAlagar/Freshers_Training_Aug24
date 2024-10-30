using BookstoreApplication.Models;
using BookstoreApplication.Service.Implementation;
using BookstoreApplication.Service.Interface;
using Microsoft.AspNetCore.Mvc;

namespace BookstoreApplication.Controllers
{
    [ApiController]
    [Route("api/[Controller]")]
    public class ConfigController : Controller
    {
        private readonly IConfigService _configService;
        public ConfigController(IConfigService _configService)
        {
            this._configService = _configService;
        }
        [HttpGet("GetAllConfigValues")]
        public async Task<IActionResult> GetAllConfigValuesAsync()
        {
            try
            {
                var res = Ok(new ApiResponse<IEnumerable<Config>>("Retreived successfully", 200, await _configService.GetAllConfigValues()));
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
