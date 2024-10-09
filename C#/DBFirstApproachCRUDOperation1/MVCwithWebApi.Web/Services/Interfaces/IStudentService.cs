using Microsoft.AspNetCore.Mvc;
using Microsoft.Graph;
using MVCwithWebApi.Web.Models;
using System.Web.Mvc;

namespace MVCwithWebApi.Web.Services.Interfaces
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task AddStudent(Student student);
        Task UpdateStudent(int id, Student student);
        Task DeleteStudent(int StudentId);
        Task<Student> GetbyId(int StudentId);

    }
}
