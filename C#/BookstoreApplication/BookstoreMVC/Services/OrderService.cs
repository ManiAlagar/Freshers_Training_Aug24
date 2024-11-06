
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using Order = BookstoreMVC.Models.Order;


namespace BookstoreMVC.Services
{
    public class OrderService: IOrderService
    {
        private readonly HttpClient _client = new();

        public OrderService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }

        public async Task<int> AddOrder(string address, string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync("/api/Order/AddOrder?Address=" + address, null);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<int>));
            return result.data;
        }

        public async Task DeleteOrder(int id, string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync("/api/Order/DeleteOrder/" + id);
        }

        public async Task<IEnumerable<Order>> GetAllOrders(string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Order/GetAllOrders");
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<List<Order>>));// ApiResponse<int>
            return result.data;
        }

        public async Task<IEnumerable<Order>> GetBooksFromOrder(int id, string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Order/GetBooksFromOrder/" + id);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<List<Order>>));
            return result.data;
        }
        public async Task UpdateOrder(int orderId,int statusId, string? token)
        {
            
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
           
            var response = await _client.PostAsync("/api/Order/UpdateOrder?orderId=" + orderId+ "&statusId=" + statusId,null);
        }



    }
}
