
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Newtonsoft.Json;
using System.Net.Http.Headers;

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
    }
}
