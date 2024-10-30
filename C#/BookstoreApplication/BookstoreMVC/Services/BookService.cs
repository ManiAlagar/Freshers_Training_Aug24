
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using Microsoft.AspNet.SignalR.Json;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Book = BookstoreMVC.Models.Book;

namespace BookstoreMVC.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _client=new();
        public BookService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }
        public async Task<IEnumerable<Book>> GetAllBooks(string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenRes.token);
           // var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.GetAsync("/api/Book/GetAllBooks");
            string responseContent = await response.Content.ReadAsStringAsync();


            //var result = JObject.Parse(responseContent);
            //string resString = JsonConvert.SerializeObject(result["data"]);
            //var response1 = JsonConvert.DeserializeObject<List<Book>>(resString);
            //return response1;
            
            dynamic res= JsonConvert.DeserializeObject(responseContent, typeof(ApiResponse<List<Book>>));
            return res.data;
        }
        public async Task AddBook(Book Book, string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(Book), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Book/AddBook", stringContent);
            string responseContent = await response.Content.ReadAsStringAsync();
            
        }
        public async Task<Book> GetBookById(int Id, string? token)
        {
           // var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer",token);
            var response = await _client.GetAsync("/api/Book/GetBookById/" + Id);
            return await response.ReadContentAsync<Book>();
        }
        public async Task UpdateBook(int id, Book Book, string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(Book), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Book/UpdateBook/" + id, stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        public async Task DeleteBook(int id, string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.DeleteAsync("/api/Book/DeleteBook/" + id);
        }

        
        public async Task IsPublish(int id, string? token)
        {
            //var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await _client.PostAsync("/api/Book/IsPublish?bookId=" + id, null);
            var responseContent = await response.Content.ReadAsStringAsync();
       
        }

    }
}
// http://localhost:5287/api/Book/IsPublish?bookId=2