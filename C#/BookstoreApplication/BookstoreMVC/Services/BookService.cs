using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace BookstoreMVC.Services
{
    public class BookService : IBookService
    {
        private readonly HttpClient _client;
        public BookService(HttpClient client)
        {
            _client = client;
        }
        public async Task<IEnumerable<Book>> GetAllBooks(string? token)
        {
            var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenRes.token);
            var response = await _client.GetAsync("/api/Book/GetAllBooks");
            return await response.ReadContentAsync<List<Book>>();
        }
        public async Task AddBook(Book Book, string? token)
        {
            var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenRes.token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(Book), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("/api/Book/AddBook", stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
        }
        public async Task<Book> GetBookById(int Id, string? token)
        {
            var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenRes.token);
            var response = await _client.GetAsync("/api/Book/GetBookById/" + Id);
            return await response.ReadContentAsync<Book>();
        }
        public async Task UpdateBook(int id, Book Book, string? token)
        {
            var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenRes.token);
            var stringContent = new StringContent(JsonConvert.SerializeObject(Book), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Book/UpdateBook/" + id, stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
        }
        public async Task DeleteBook(int id, string? token)
        {
            var tokenRes = JsonConvert.DeserializeObject<TokenResponse>(token);
            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", tokenRes.token);
            var response = await _client.DeleteAsync("/api/Book/DeleteBook/" + id);
        }
    }
}
