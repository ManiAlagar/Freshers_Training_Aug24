using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using MVCwithWebApi.Web.Helpers;

namespace BookstoreMVC.Services
{
    public class StatusService :IStatusService
    {
        private readonly HttpClient _client = new();
        public StatusService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }

        public async Task<IEnumerable<Status>> GetAllOrderStatus()
        {
            var response = await _client.GetAsync("/api/Status/GetAllOrderStatus");
            return await response.ReadContentAsync<List<Status>>();
        }

        

    }
}
