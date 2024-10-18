using Expense_Tracker_MVC.Models;
using Newtonsoft.Json;
using System.Security.Claims;
using Expense_Tracker_MVC.Service.Interface;
using Expense_Tracker_MVC.Helpers;
using System.Net.Http.Headers;

namespace Expense_Tracker_MVC.Service.Implement
{
    public class CategoryService : ICategoryService
    {
        
        private readonly HttpClient client;
        private readonly IHttpContextAccessor httpContextAccessor;
        public CategoryService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            this.client = client;
            this.httpContextAccessor = httpContextAccessor;
        }

        public async Task<IEnumerable<Category>> Get()
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);

            CategoryHelper obj = new(client, httpContextAccessor);
            string url = $"https://localhost:7273/api/Category/Get?id={UserID}";

            var data = await obj.Get(url);

            //var reponse = ;

            return JsonConvert.DeserializeObject<IEnumerable<Category>>(data);
 
        }

        public async Task<Category> Get(int? id)
        {
            CategoryHelper obj = new(client, httpContextAccessor);

            var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


            var response = await client.GetAsync($"https://localhost:7273/api/Category/GetByID?id={id}");

            return await response.ReadContentAsync<Category>();
        }


        public async Task Create(Category entity)
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
            entity.UserId = Convert.ToInt32(UserID);

            CategoryHelper obj = new(client, httpContextAccessor);
   
            string url = "https://localhost:7273/api/Category/Create";

            await obj.Post(entity, url);
        }

        public async Task Edit(Category entity)
        {
            CategoryHelper obj = new(client, httpContextAccessor);
            string url = $"https://localhost:7273/api/Category/Edit/{entity.CategoryID}";

            await obj.Put(entity, url);
        }

        public async Task Delete(int id)
        {
            CategoryHelper obj = new(client, httpContextAccessor);

            //var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
            //client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            obj.isValid();
        

            var response = await client.DeleteAsync($"https://localhost:7273/api/Category/Delete/{id}");
        }


    }
}
