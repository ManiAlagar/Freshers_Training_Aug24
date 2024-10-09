
using Azure;
using DBFirstApproachCRUD.Models;
using Microsoft.AspNetCore.Mvc;
using MVCwithWebApi.Web.Helpers;
using MVCwithWebApi.Web.Models;
using MVCwithWebApi.Web.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;
using System.Web.Mvc;
using Student = MVCwithWebApi.Web.Models.Student;


namespace MVCwithWebApi.Web.Services
{
    public class StudentService: IStudentService
    {
        private readonly HttpClient _client;
       

        public StudentService(HttpClient client)
        {
            _client = client;
        }



        public async Task DeleteStudent(int id)
        {
            var response = await _client.DeleteAsync("/api/Student/DeleteStudent/" + id);

        }

        public async Task<IEnumerable<Student>> GetAll()
        {
            var response = await _client.GetAsync("/api/Student/GetAll");
            return await response.ReadContentAsync<List<Student>>();
        }

        public async Task<Student> GetbyId(int StudentId)
        {
            var response = await _client.GetAsync("/api/Student/GetByStudentID/"+StudentId);
            return await response.ReadContentAsync<Student>();
        }

        public async Task UpdateStudent(int id,Student student)
        {
            var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = await _client.PutAsync("/api/Student/UpdateStudent/" + id, stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
        }

        public async Task AddStudent(Student student)
        {
         
            var stringContent = new StringContent(JsonConvert.SerializeObject(student), Encoding.UTF8, "application/json");
            var response = await _client.PostAsync("https://localhost:7092/api/Student/CreateStudent", stringContent);
            var responseContent = await response.Content.ReadAsStringAsync();
             
        }
    }
}
