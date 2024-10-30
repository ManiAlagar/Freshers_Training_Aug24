using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Services.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Expense_Tracker_API.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CategorySpendController : ControllerBase
    {
        private readonly ICategorySpendService categorySpendService;

        public CategorySpendController(ICategorySpendService categorySpendService)
        { 
            this.categorySpendService = categorySpendService;
        }

        [HttpGet("Get")]
        public async Task<IEnumerable<CategorySpend>> Get(int id)
        {
            var entity = await categorySpendService.Get(id);

            return entity;

        }
    }
}
