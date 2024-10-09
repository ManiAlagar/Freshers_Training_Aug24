using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MVCwithWebApi.Web.Models;
using System.Web.Mvc;

namespace MVCwithWebApi.Web.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll(string? token);
        Task AddStudent(Student student, string? token);
        Task UpdateStudent(int id, Student student, string? token);
        Task DeleteStudent(int StudentId, string? token);
        Task<Student> GetbyId(int StudentId, string? token);
        Task<string> Login (Student student);

    }
}
