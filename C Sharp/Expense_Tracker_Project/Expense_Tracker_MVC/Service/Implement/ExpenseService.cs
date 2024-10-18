using Expense_Tracker_MVC.Helpers;
using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Expense_Tracker_MVC.Service.Implement
{
    public class ExpenseService : IExpenseService
    {

        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public ExpenseService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Expenses>> Get()
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);

            ExpenseHelper obj = new(client, httpContextAccessor);
            string url = $"https://localhost:7273/api/Expense/Get?id={UserID}";

            var data = await obj.Get(url);

            return JsonConvert.DeserializeObject<IEnumerable<Expenses>>(data);

        }


        public async  Task<Expenses> GetByID(int? id)
        {
            ExpenseHelper obj = new(client, httpContextAccessor);

            var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync($"https://localhost:7273/api/Expense/GetByID?id={id}");

            return await response.ReadContentAsync<Expenses>();
        }




        public Task Create(Expenses entity)
        {
            throw new NotImplementedException();
        }

        public Task Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task Edit(Expenses entity)
        {
            throw new NotImplementedException();
        }
    }
}
