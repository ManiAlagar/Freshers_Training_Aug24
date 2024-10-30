using Expense_Tracker_MVC.Helpers;
using Expense_Tracker_MVC.Models;
using Expense_Tracker_MVC.Service.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;

namespace Expense_Tracker_MVC.Service.Implement
{
    public class BudgetService : IBudgetService
    {

        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BudgetService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Budget>> Get()
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);

            BudgetHelper obj = new(client, httpContextAccessor);
            string url = $"https://localhost:7273/api/Budget/Get?id={UserID}";

            var data = await obj.Get(url);

            try
            {

                return JsonConvert.DeserializeObject<IEnumerable<Budget>>(data);
            }
            catch { return null; }

		}

        public async Task<Budget> GetByID(int? id)
        {

            BudgetHelper obj = new(client, httpContextAccessor);

            //var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            obj.isValid();


            var response = await client.GetAsync($"https://localhost:7273/api/Budget/GetByID?id={id}");

            return await response.ReadContentAsync<Budget>();
        }


        public async Task<string> Create(Budget entity)
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
            entity.UserID = Convert.ToInt32(UserID);
            entity.CategoryID = Convert.ToInt32(entity.CategoryName);
            entity.Balance = entity.BudgetAmount;

            BudgetHelper obj = new(client, httpContextAccessor);

            string url = "https://localhost:7273/api/Budget/Create";

            return await obj.Post(entity, url);
        }

        public async Task<string> Edit(Budget entity)
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
            entity.UserID = Convert.ToInt32(UserID);
            entity.CategoryID = Convert.ToInt32(entity.CategoryName);

            BudgetHelper obj = new(client, httpContextAccessor);
            string url = $"https://localhost:7273/api/Budget/Edit/{entity.Id}";

            return await obj.Put(entity, url);
        }


        public async Task<string> Delete(int id)
        {

            BudgetHelper obj = new(client, httpContextAccessor);
            obj.isValid();

            var response = await client.DeleteAsync($"https://localhost:7273/api/Budget/Delete/{id}");

            var a = await response.Content.ReadAsStringAsync();

            //dynamic status = JsonConvert.DeserializeObject(a);

            return a;



            return response.ToString();
        }


    }
}
