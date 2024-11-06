using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
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

        public async Task<int> Register(User user)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/User/Register", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<int>));
            return result.data;
        }

        public async Task<User> GetUserById(int Id, string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/User/GetUserById/" + Id);
            return await response.ReadContentAsync<User>();
        }


        public async Task<int> UpdateUser(int id, User user, string? token)
        {
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(user), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/User/UpdateUser/" + id, stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<int>));
            return result.data;
        }
    }
}
