using Expense_Tracker_MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Helper.Helpers
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


        public async Task<bool> Post(Users entity,string url)
        {
                isValid();
                entity.UserID = 0;
                var serializedData = JsonConvert.SerializeObject(entity);
                var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

                var response = await _client.PostAsync(url, result);

                if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
                {
                    return false;            
                }
                return true;
        }


        public async Task<bool>  Put(Users entity, string url)
        {
            isValid();
                
            var serializedData = JsonConvert.SerializeObject(entity);
            var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(url, result);

            var data = await response.Content.ReadAsStringAsync();
            return Convert.ToBoolean(data);
        }

    }
}
