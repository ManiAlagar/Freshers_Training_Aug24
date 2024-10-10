using CRUD_USER.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ActionConstraints;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace CRUD_USER.Helpers
{
    public static class HttpClientExtensions
    {
        public static async Task<T> ReadContentAsync<T>(this HttpResponseMessage response)
         {
            if (response.IsSuccessStatusCode == false)
                throw new ApplicationException($"Something went wrong");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            var result = JsonSerializer.Deserialize<T>
            (
            dataAsString, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });
            return result;
        }                
    }
}
