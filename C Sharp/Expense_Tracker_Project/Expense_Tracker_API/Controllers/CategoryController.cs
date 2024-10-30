
using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;


namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryService categoryService;


        public CategoryController(ICategoryService categoryService)
        {
            this.categoryService = categoryService;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<Category>> Get(int id)
        {
            var entity = await categoryService.Get(id);

            return entity;

        }

        [HttpGet("GetByID")]
        public async Task<Category> GetByID(int id)
        {
            var entity = await categoryService.GetByID(id);

            return entity;
        }

        [HttpPost("Create")]
        public async Task<IActionResult> Create([FromBody] Category entity)
        {
            if (entity == null)
            {
                return BadRequest("Employee is null");
            }

            string status = await categoryService.Add(entity);

            return Ok(status);
        }

        [HttpPut("Edit/{id}")]
        public async Task<IActionResult> update(int id, [FromBody] Category entity)
        {
            Category category = await categoryService.GetByID(id);

            if (category == null)
            {
                return NotFound("The Employee record couldn't be found.");
            }

            string status = await categoryService.Edit(entity);

            return Ok(status);
        }

        // DELETE METHOD
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
           
            var status = await categoryService.Delete(id);

            return Ok(status);
        }
    }
}

