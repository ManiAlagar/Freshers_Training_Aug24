using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Newtonsoft.Json;
using System.Text;

namespace BookstoreMVC.Services
{
    public class UserService:IUserService
    {
        private readonly HttpClient _client;
        public UserService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }
        public async Task<string> Login(LoginModel user)
        {
            try
            {
                var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
                var response = await _client.PostAsync("/api/User/Login", stringContent);
                var token = await response.Content.ReadAsStringAsync();
                return token;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string> Register(User user)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/User/Register", stringContent);
            return await response.Content.ReadAsStringAsync();
        }
    }
}
