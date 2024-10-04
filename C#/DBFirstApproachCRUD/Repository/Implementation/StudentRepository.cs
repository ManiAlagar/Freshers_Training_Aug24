using DBFirstApproachCRUD.Context;
using DBFirstApproachCRUD.Models;
using DBFirstApproachCRUD.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace DBFirstApproachCRUD.Repository.Implementation
{
    public class StudentRepository: IStudentRepository
    {
        public readonly StudentDbContext db;

        public StudentRepository(StudentDbContext context)
        {
            db = context;
        }

        public async Task<Student> AddStudent(Student Student)
        {
            var newStudent = new Student()
            {
                StudentName = Student.StudentName,
                Email = Student.Email,
                Password = Student.Password
            };
            await db.Students.AddAsync(newStudent);
            await db.SaveChangesAsync();
            return newStudent;
        }

       
        

        public async Task<IEnumerable<Student>> GetAll()
        {
            return await db.Students.ToListAsync();
        }

        public async Task<Student> GetStudent(int StudentId)
        {
            var student = await db.Students.FindAsync(StudentId);
            return student;
        }

        public async Task<Student> UpdateStudent(int StudentId, Student Student)
        {
            var existingStudent = await db.Students.FindAsync(StudentId);
            if (existingStudent != null)
            {
                existingStudent.StudentName = Student.StudentName;
                existingStudent.Email = Student.Email;
                existingStudent.Password = Student.Password;
                await db.SaveChangesAsync();
                return existingStudent;
            }
            return null;
        }

        public async Task<Student> DeleteStudent(int id)
        {
            var StudentUser = await db.Students.FindAsync(id);
            if (StudentUser != null)
            {
                db.Remove(StudentUser);
                await db.SaveChangesAsync();

            }
            return StudentUser;
        }


    }
}
