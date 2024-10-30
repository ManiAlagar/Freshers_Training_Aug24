
using BookstoreMVC.Models;
using BookstoreMVC.Services.Interface;
using MVCwithWebApi.Web.Helpers;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace BookstoreMVC.Services
{
    public class RoleService : IRoleService
    {
        private readonly HttpClient _client = new();
        public RoleService()
        {
            _client = new();
            _client.BaseAddress = new Uri("http://localhost:5287/");
        }


        async Task<IEnumerable<Role>> IRoleService.GetAllRoles()
        {
            var response = await _client.GetAsync("/api/Role/GetAllRoles");
            return await response.ReadContentAsync<List<Role>>();
        }
    }
}
