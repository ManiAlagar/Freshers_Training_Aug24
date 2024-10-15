using BookstoreApplication.Models;
using BookstoreApplication.Service.Interface;
using BookstoreMVC.Models;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookstoreMVC.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _client;
        public RoleService(HttpClient client)
        {
            _client = client;
        }

         async Task<IEnumerable<BookstoreApplication.Models.Role>> IRoleService.GetAllRoles()
        {
            var response = await _client.GetAsync("/api/Role/GetAllRoles");
            return await response.ReadContentAsync<List<BookstoreApplication.Models.Role>>();
        }
    }
}
