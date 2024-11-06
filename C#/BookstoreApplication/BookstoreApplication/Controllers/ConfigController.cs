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

        [HttpPut]
        [Route("UpdateConfig/{id:int}")]
        public async Task<ActionResult<int?>> UpdateConfig([FromRoute] int id,Config config)
        {
            try
            {
                if (id == null)
                    return BadRequest("UpdateConfig ID mismatch");

                var ToUpdate = await _configService.GetConfigById(id);

                if (ToUpdate == null)
                    return NotFound($"UpdateConfig with Id = {id} not found");

                var updated = await _configService.UpdateConfig(id, config);
                var res = new ApiResponse<int>("Updated successfully", 200, updated);
                return Ok(res);
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    "Error updating data");
            }
        }


        [HttpGet]
        [Route("GetConfigById/{id:int}")]
        public async Task<ActionResult<Config?>> GetConfigById([FromRoute] int id)

        {
            try
            {
                var result = await _configService.GetConfigById(id);

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
