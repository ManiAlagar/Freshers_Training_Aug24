using Student_CRUD_API.Entity;
using Student_CRUD_API.Repository.Implementation;
using Student_CRUD_API.Repository.Interface;
using Student_CRUD_API.Services.Interface;

namespace Student_CRUD_API.Services.Implementation
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepostiory studentRepostiory;
        public StudentService(IStudentRepostiory studentRepostiory)
        {
            this.studentRepostiory = studentRepostiory;
        }


        public async Task<IEnumerable<Students>> Get()
        {
            return await studentRepostiory.Get();
        }
        public async Task<bool> AddStudent(Students entity)
        {
            return  await studentRepostiory.AddStudent(entity);
        }

        public async Task<Students> GetStudent(int id)
        {
            return await studentRepostiory.GetStudent(id);
        }

        public async Task DeleteStudent(Students student)
        {
            await studentRepostiory.DeleteStudent(student);
        }

        public async  Task UpdateStudent(Students student)
        {
            await studentRepostiory.UpdateStudent(student);
        }
    }
}