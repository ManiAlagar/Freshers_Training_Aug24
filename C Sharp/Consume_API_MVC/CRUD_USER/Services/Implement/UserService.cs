using CRUD_USER.Helpers;
using CRUD_USER.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using Users_CRUD.Web.Services.Interfaces;

namespace Users_CRUD.Web.Services.Implement
{
    public class UserService : IUserService
    {    
            private readonly HttpClient _client;
            private readonly IHttpContextAccessor httpContextAccessor;
            public const string BasePath = "/api/User/GetAll";

            public UserService(HttpClient client, IHttpContextAccessor httpContextAccessor)
            {
                _client = client ?? throw new ArgumentNullException(nameof(client));
                this.httpContextAccessor = httpContextAccessor;

            }

            public async Task<string> Login(Login credenntial)
            {

                var response = await _client.GetAsync($" https://localhost:7213/api/Login/Login?UserName={credenntial.UserName}&Password={credenntial.Password}");
                var result = await response.Content.ReadAsStringAsync();

                var token = JsonConvert.DeserializeObject<TokenResponse>(result);


                if (response.IsSuccessStatusCode)
                {
                    return token.token;
                }
                return null;
            }


            public async Task<IEnumerable<Users>> Get()
            {
            // var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                 HTTPHelper obj = new(_client, httpContextAccessor);
                 string url = "/api/User/GetAll";

                 var data = await obj.Get(url);
                    
                if(data != "Unauthorized")
                {
                    var reposne = JsonConvert.DeserializeObject<List<Users>>(data);

                    return reposne;
                }

              return null;

                //var response = await _client.GetAsync("/api/User/GetAll");
                /*return await response.ReadContentAsync<List<Users>>();*/
            }

            public async Task<Users> Get(int? id)
            {
                var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                var response = await _client.GetAsync($"api/User/GetByID?id={id}");

                return await response.ReadContentAsync<Users>();
            }


            public async Task Create(Users entity)
            {
                var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

                //var serializedData = JsonConvert.SerializeObject(entity);
                //var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

                HTTPHelper obj = new(_client, httpContextAccessor);
                string url = "https://localhost:7213/api/User/Create";

                await obj.Post(entity,url);
                

                //var response = await _client.PostAsync("https://localhost:7213/api/User/Create", result);
            }

            public async Task Edit(Users entity)
            {

                var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);


                HTTPHelper obj = new(_client, httpContextAccessor);
                string url = $"https://localhost:7213/api/User/Edit/{entity.ID}";

                await obj.Put(entity, url);

                //var serializedData = JsonConvert.SerializeObject(entity);
                //var result = new StringContent(serializedData, Encoding.UTF8,"application/json");

                //var response = await _client.PutAsync($"https://localhost:7213/api/User/Edit/{entity.ID}",result);

            }

            public async Task Delete(int id)
            {
                var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
                _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await _client.DeleteAsync($"/api/User/Delete/{id}");
            }


         
    }
}
