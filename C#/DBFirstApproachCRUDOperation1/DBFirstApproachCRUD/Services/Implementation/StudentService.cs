using DBFirstApproachCRUD.Models;
using DBFirstApproachCRUD.Repository.Implementation;
using DBFirstApproachCRUD.Repository.Interface;
using DBFirstApproachCRUD.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace DBFirstApproachCRUD.Services.Implementation
{
    public class StudentService : IStudentService
    {

        private readonly IStudentRepository studentRepository;
        public StudentService(IStudentRepository StudentRepository)
        {
            this.studentRepository = StudentRepository;
        }


        public async Task<IEnumerable<Student>> GetAll()
        {
            return await studentRepository.GetAll();
        }

        public async Task<Student> AddStudent(Student student)
        {
            return await studentRepository.AddStudent(student);
        }

        public async Task<Student> GetStudent(int StudentId)
        {
            return await studentRepository.GetStudent(StudentId);
        }


        public async Task<Student> UpdateStudent(int StudentId, Student Student)
        {
            return await studentRepository.UpdateStudent(StudentId, Student);
        }

        public async Task<Student> DeleteStudent(int id)
        {
            return await studentRepository.DeleteStudent(id);
        }


    }
}