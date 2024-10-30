using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class BudgetController : ControllerBase
    {
        private readonly IBudgetService budgetService;
        public BudgetController(IBudgetService budgetService)
        {
            this.budgetService = budgetService;
        }

        // GetAllDetails METHOD
        [HttpGet("Get")]
        public async Task<IEnumerable<Budget>> Get(int id)
        {
            var entity = await budgetService.Get(id);

            return entity;

        }

        // GETByID METHOD
        [HttpGet("GetByID")]
        public async Task<IActionResult> GetByID(int id)
        {
            var entity  = await budgetService.GetByID(id);
            return Ok(entity);
        }

        // CREATE METHOD
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Budget entity)
        {
            if (entity == null)
            {
                return BadRequest("Employee is null");
            }

            string status = await budgetService.Add(entity);

            return Ok(status);
        }

        // EDIT METHOD
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Budget entity)
        {
            //Budget budget = await budgetService.GetByID(id);

            //if (budget == null)
            //{
            //    return NotFound("The Employee record couldn't be found.");
            //}

            string status = await budgetService.Edit(entity);

            return Ok(status);
        }


        // DELETE METHOD
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            string status = await budgetService.Delete(id);
            return Ok(status);
        }

    }
}
