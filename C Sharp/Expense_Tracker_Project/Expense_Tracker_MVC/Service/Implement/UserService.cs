
using Expense_Tracker_MVC.Service.Interface;
using Expense_Tracker_MVC.Helpers;
using Expense_Tracker_MVC.Models;
using Newtonsoft.Json;
using Helper.Helpers;
using System.Net.Http.Headers;
using System.Security.Claims;


namespace Expense_Tracker_MVC.Service.Implement
{
    public class UserService : IUserService
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor httpContextAccessor;
        

        public UserService(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            this.httpContextAccessor = httpContextAccessor;
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
        }

        public async Task<string> Login(Login credenntial)
        {
         
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7273/api/Login/Login");
            //requestMessage.Headers.Add("UserName", credenntial.Username);
            requestMessage.Headers.Add("Email", credenntial.Email);
            requestMessage.Headers.Add("Password", credenntial.Password);

            var response = await _client.SendAsync(requestMessage);
            var result = await response.Content.ReadAsStringAsync();

            var token = JsonConvert.DeserializeObject<TokenResponse>(result);

            if (response.IsSuccessStatusCode)
            {
                return token.token;
            }
            return null; 
        }

        public async Task<Users> Get(int? id)
        {
            var UserID = (httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid)?.Value);
            var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync($"api/User/GetByID?id={UserID}");

            return await response.ReadContentAsync<Users>();
        }

        public async Task<bool> Create(Users entity)
        { 

            HTTPHelper obj = new(_client, httpContextAccessor);
            string url = "https://localhost:7273/api/User/Create";

            bool flag = await obj.Post(entity, url);

            return flag;
        }

        public async Task<bool> Edit(Users entity)
        {
            
            HTTPHelper obj = new(_client, httpContextAccessor);
            string url = $"https://localhost:7273/api/User/Edit/{entity.UserID}";

           var status = await obj.Put(entity, url);
           return status;
        }
    }
}
