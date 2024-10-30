using Expense_Tracker_MVC.Helpers;
using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Newtonsoft.Json;
using System.Security.Claims;

namespace Expense_Tracker_MVC.Service.Implement
{
    public class CategorySpendService : ICategorySpendService
    {
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CategorySpendService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<CategorySpend>> Get()
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);

            CategoryHelper obj = new(client, httpContextAccessor);
            string url = $"https://localhost:7273/api/CategorySpend/Get?id={UserID}";

            var data = await obj.Get(url);

            //var reponse = ;

            return JsonConvert.DeserializeObject<IEnumerable<CategorySpend>>(data);

        }
    }
}
