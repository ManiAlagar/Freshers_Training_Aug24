using CRUD_USER.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;


namespace CRUD_USER.Helpers
{
    public class HTTPHelper
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public HTTPHelper(HttpClient client, IHttpContextAccessor httpContextAccessor)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            this.httpContextAccessor = httpContextAccessor;
        }

        public HTTPHelper()
        {
        }

        public string isValid()
        {
            try
            {
                var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;
                if (!string.IsNullOrWhiteSpace(token))
                {
                    _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }
                return token;
            }
            catch
            {
                return null;            
            }
            
        }
     

        public async Task<string> Get(string url)
        {
            if (isValid() is null)
            {
                return "Unauthorized";
            }
            var response =  await _client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }


        public async Task Post(Users entity,string url)
        {
            if (isValid() != null)
            {
                var serializedData = JsonConvert.SerializeObject(entity);
                var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(url, result);
            }
        }


        public async Task Put(Users entity, string url)
        {
            if (isValid() != null)
            {
                var serializedData = JsonConvert.SerializeObject(entity);
                var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var response = await _client.PutAsync(url, result);
            }
                
        }
    }
}
