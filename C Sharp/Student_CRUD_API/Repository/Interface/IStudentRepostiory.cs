using Student_CRUD_API.Entity;

namespace Student_CRUD_API.Repository.Interface
{
    public interface IStudentRepostiory
    {
        Task<IEnumerable<Students>> Get();

        Task<bool> AddStudent(Students entity);

        Task<Students> GetStudent(int id);

        Task DeleteStudent(Students student);

        Task UpdateStudent(Students student);
    }
}
