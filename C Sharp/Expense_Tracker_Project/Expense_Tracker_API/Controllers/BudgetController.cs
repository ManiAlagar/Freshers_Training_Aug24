using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
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
        public async Task<Budget> GetByID(int id)
        {
            var entity  = await budgetService.GetByID(id);
            return entity;
        }

        // CREATE METHOD
        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Budget entity)
        {
            if (entity == null)
            {
                return BadRequest("Employee is null");
            }

            await budgetService.Add(entity);

            return Ok(entity);
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

            await budgetService.Edit(entity);

            return Ok("Updated Successful");
        }


        // DELETE METHOD
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            await budgetService.Delete(id);
            return Ok("Record Deleted");
        }

    }
}
