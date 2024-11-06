
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BookstoreMVC.Services
{
    public class ConfigService : IConfigService
    {
        private readonly HttpClient _client = new();
        public ConfigService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }
        public async Task<IEnumerable<Config>> GetAllConfigValues(string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Config/GetAllConfigValues");
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic res = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<List<Config>>));
            return res.data;
        }

        public async Task<Config> GetConfigById(int Id, string? token)
        {
            // var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Config/GetConfigById/" + Id);
            return await response.ReadContentAsync<Config>();
        }
        public async Task<int> UpdateConfig(int id, Config config, string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(config), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Config/UpdateConfig/" + id, stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<int>));
            return result.data;
        }

        

    }
}
