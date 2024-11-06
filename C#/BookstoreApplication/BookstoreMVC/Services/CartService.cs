using Azure;
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using Cart = BookstoreMVC.Models.Cart;


namespace BookstoreMVC.Services
{
    public class CartService : ICartService
    {
        private readonly HttpClient _client= new();

        public CartService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }
        public async Task<IEnumerable<Cart>> GetAllCartItems(string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Cart/GetAllCartItems");
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic res = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<List<Cart>>));
            return res.data;
        }
        public async Task<IEnumerable<Discount>> Discount(string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Cart/Discount");
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic res = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<List<Discount>>));
            return res.data;
        }

        public async Task<Cart> GetCartItemById(int Id, string? token)
        {
           // var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.GetAsync("/api/Cart/GetCartItemById/" + Id);
            return await response.ReadContentAsync<Cart>();
        }
        public async Task DeleteItemFromCart(int id, string? token)
        {
           // var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync("/api/Cart/DeleteItemFromCart/" + id);
        }

        public async Task AddItemToCart(int id, string? token)
        {
           // var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.PostAsync("/api/Cart/AddItemToCart?bookId="+id,null);
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        public async Task<int> UpdateQuantity(int id,int quantity, string? token)
        {
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync("/api/Cart/UpdateQuantity?cartItemId=" + id + "&quantity="+quantity,null);
            string responseContent = await response.Content.ReadAsStringAsync();
            dynamic result = JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<int>));
            return result.data;
        }

        
        
    }
}
