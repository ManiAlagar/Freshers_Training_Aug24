using DBFirstApproachCRUD.Models;
using Microsoft.AspNetCore.Mvc;

namespace DBFirstApproachCRUD.Services.Interface
{
    public interface IStudentService
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetStudent(int StudentId);
        Task<Student> AddStudent(Student User);
        Task<Student> UpdateStudent(int StudentId, Student Student);
        Task<Student> DeleteStudent(int id);
       
    }
}
