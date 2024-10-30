using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class ExpenseController : ControllerBase
    {
        private readonly IExpenseService expenseService;
        public ExpenseController(IExpenseService expenseService)
        {
            this.expenseService = expenseService;
        }


		[HttpGet("GetMonthly")]
		public async Task<IEnumerable<Expenses>> GetMonthly(int id)
		{
			try
			{
				var entity = await expenseService.GetMonthly(id);

				return entity;
			}
			catch (Exception Message)
			{
				throw Message;
			}
		}

		[HttpGet("Get")]
        public async Task<IEnumerable<Expenses>> Get(int id)
        {
            try
            {
                var entity = await expenseService.Get(id);

                return entity;
            }
            catch (Exception Message)
            {
                throw Message;
            }
        }


        [HttpGet("GetByID")]
        public async Task<Expenses> GetByID(int id)
        {
            try
            {
                var entity = await expenseService.GetByID(id);
                //entity.Date = entity.Date;
                return entity;
            }
            catch (Exception Message)
            {
                throw Message;
            }
        }

        // CREATE METHOD
        [HttpPost("Create")]
        public async Task<IActionResult>  Create([FromBody] Expenses entity)
        {
            //if (entity == null)
            //{
            //    return BadRequest("Employee is null");
            //}

             string status = await expenseService.Add(entity);

            //return status;
            return Ok(new {entity,status});
        }

        // EDIT METHOD
        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Budget entity)
        {
            var expense = await expenseService.GetByID(id);

            //if (expense == null)
            //{
            //    return NotFound("The Employee record couldn't be found.");
            //}

            await expenseService.Edit(expense);

            return Ok("Updated Successful");
        }



        // DELETE METHOD
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await expenseService.Delete(id);
            return Ok("Record Deleted");
        }

    }
}
