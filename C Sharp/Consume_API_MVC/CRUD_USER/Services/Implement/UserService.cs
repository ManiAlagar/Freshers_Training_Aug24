using CRUD_USER.Helpers;
using CRUD_USER.Models;
using Newtonsoft.Json;
using System.Text;
using Users_CRUD.Web.Services.Interfaces;

namespace Users_CRUD.Web.Services.Implement
{
    public class UserService : IUserService
    {    
            private readonly HttpClient _client;
            public const string BasePath = "/api/User/GetAll";

            public UserService(HttpClient client)
            {
                _client = client /*?? throw new ArgumentNullException(nameof(client))*/;
            }

            public async Task<IEnumerable<Users>> Get()
            {
                var response = await _client.GetAsync("/api/User/GetAll");

                return await response.ReadContentAsync<List<Users>>();
            }

            public async Task<Users> Get(int? id)
            {
                var response = await _client.GetAsync($"api/User/GetByID?id={id}");

                return await response.ReadContentAsync<Users>();
            }


            public async Task Create(Users entity)
            {
                using var client = new HttpClient();

                var serializedData = JsonConvert.SerializeObject(entity);
                var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var response = await client.PostAsync("https://localhost:7213/api/User/Create", result);
                var responseString = await response.Content.ReadAsStringAsync();

                await response.ReadContentAsync<Users>();

                response.EnsureSuccessStatusCode();
            }

            public async Task Edit(Users entity)
            {
                using var client = new HttpClient();

                var serializedData = JsonConvert.SerializeObject(entity);
                var result = new StringContent(serializedData, Encoding.UTF8,"application/json");

                var response = await client.PutAsync($"https://localhost:7213/api/User/Edit/{entity.ID}",result);
                var responseString = await response.Content.ReadAsStringAsync();

               response.EnsureSuccessStatusCode();
            }

            public async Task Delete(int id)
            {
                var response = await _client.DeleteAsync($"/api/User/Delete/{id}");
            }
    }
}
