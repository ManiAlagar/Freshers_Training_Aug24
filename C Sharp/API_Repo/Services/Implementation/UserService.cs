using CRUD_Web_API.Entity;
using CRUD_Web_API.Repository.Interface;
using CRUD_Web_API.Services.Interface;

namespace CRUD_Web_API.Services.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;   
        }

        public void Add(Users entity)
        {
            userRepository.Add(entity);
        }

        public void Delete(Users entity)
        {
            userRepository.Delete(entity);
        }

        public void Edit(Users dbEntity, Users entity)
        {
            userRepository.Edit(dbEntity, entity);
        }

        public Users Get(long id)
        {
            return userRepository.Get(id);
        }

        public IEnumerable<Users> GetAll()
        {
            return userRepository.GetAll();
        }
    }
}
