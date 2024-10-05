using Microsoft.EntityFrameworkCore;
using Student_CRUD_API.Entity;
using Student_CRUD_API.Entity.DBContext;
using Student_CRUD_API.Repository.Interface;
using Student_CRUD_API.Services.Implementation;

namespace Student_CRUD_API.Repository.Implementation
{
    public class StudentRepostiory : IStudentRepostiory
    {
        private readonly StudentContext context;

        public StudentRepostiory(StudentContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<Students>> Get()
        {
            return await context.Student.ToListAsync();
        }

        public async Task<bool> AddStudent(Students entity)
        {
            try
            {
                await context.Student.AddAsync(entity);
                await context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            } 
        }

        public async Task<Students> GetStudent(int id)
        {
            var student = await  context.Student.FirstOrDefaultAsync(u => u.ID == id);

            return student;
        }

        public async Task DeleteStudent(Students student)
        {
               context.Student.Remove(student);
               await context.SaveChangesAsync();
        }

        public async Task UpdateStudent(Students student)
        {
            context.Student.Update(student);
            await context.SaveChangesAsync();
        }
    }
}
