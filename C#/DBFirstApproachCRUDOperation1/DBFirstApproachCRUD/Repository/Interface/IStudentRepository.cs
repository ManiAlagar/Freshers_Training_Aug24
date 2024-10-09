using DBFirstApproachCRUD.Models;

namespace DBFirstApproachCRUD.Repository.Interface
{
    public interface IStudentRepository
    {
        Task<IEnumerable<Student>> GetAll();
        Task<Student> GetStudent(int StudentId);
        Task<Student> AddStudent(Student student);
        Task<Student> UpdateStudent(int StudentId, Student User);
        Task<Student> DeleteStudent(int id);
    }
}