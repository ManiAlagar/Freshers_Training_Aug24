using Expense_Tracker_MVC.Models;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;

namespace Expense_Tracker_MVC.Helpers
{
    public class BudgetHelper
    {
        private readonly HttpClient _client;
        private readonly IHttpContextAccessor httpContextAccessor;

        public BudgetHelper(HttpClient client, IHttpContextAccessor httpContextAccessor)
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
            
            var response = await _client.GetAsync(url);
            var data = await response.Content.ReadAsStringAsync();
            return data;
        }

        public async Task<string> Post(Budget entity, string url)
        {
            isValid();
            //var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;

            //_client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var serializedData = JsonConvert.SerializeObject(entity);
            var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync(url, result);

            var status = await response.Content.ReadAsStringAsync();

            //dynamic status = JsonConvert.DeserializeObject(a);

           


            //if (response.StatusCode != System.Net.HttpStatusCode.OK)
            //{
            //    return false;
            //}
            return status;
        }


        public async Task<string> Put(Budget entity, string url)
        {
            isValid();
            var token = httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.UserData)?.Value;

            _client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            var serializedData = JsonConvert.SerializeObject(entity);
            var result = new StringContent(serializedData, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync(url, result);
            var status = await response.Content.ReadAsStringAsync();


            return status;

        }
    }
}
