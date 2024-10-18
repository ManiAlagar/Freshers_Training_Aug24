
using Expense_Tracker_MVC.Service.Interface;
using Expense_Tracker_MVC.Helpers;
using Expense_Tracker_MVC.Models;
using Newtonsoft.Json;
using Helper.Helpers;


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

        }

        public async Task<string> Login(Login credenntial)
        {
         
            var requestMessage = new HttpRequestMessage(HttpMethod.Get, "https://localhost:7273/api/Login/Login");
            requestMessage.Headers.Add("UserName", credenntial.UserName);
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

            var response = await _client.GetAsync($"api/User/GetByID?id={id}");

            return await response.ReadContentAsync<Users>();
        }

        public async Task<bool> Create(Users entity)
        { 

            HTTPHelper obj = new(_client, httpContextAccessor);
            string url = "https://localhost:7273/api/User/Create";

            bool flag = await obj.Post(entity, url);

            return flag;
        }

        public async Task Edit(Users entity)
        {
            HTTPHelper obj = new(_client, httpContextAccessor);
            string url = "https://localhost:7273/api/User/Edit";

            await obj.Put(entity, url);

        }
    }
}
