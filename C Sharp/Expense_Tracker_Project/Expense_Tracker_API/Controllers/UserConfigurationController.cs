using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class UserConfigurationController : ControllerBase
    {
        private readonly IConfigurationService configurationService;

        public UserConfigurationController(IConfigurationService configurationService)
        {
            this.configurationService = configurationService;   
        }

        // GetAllDetails METHOD
        [HttpGet("Get")]
        public async Task<IEnumerable<UserConfiguration>> Get(int id)
        {
            var entity = await configurationService.Get(id);

            return entity;
        }

        // GetAllDetails METHOD
        [HttpGet("GetByID")]
        public async Task<UserConfiguration> GetByID(int id)
        {
            var entity = await configurationService.GetById(id);
            return entity;
        }

        // CREATE METHOD
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] UserConfiguration entity)
        {
            if (entity == null)
            {
                return BadRequest("Employee is null");
            }

            await configurationService.Add(entity);

            return Ok(entity);
        }

        // EDIT METHOD
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] UserConfiguration entity)
        {
            //Budget budget = await budgetService.GetByID(id);

            //if (budget == null)
            //{
            //    return NotFound("The Employee record couldn't be found.");
            //}

            await configurationService.Edit(entity);

            return Ok("Updated Successful");
        }


    }
}
