using CodeFirstApproachCRUD.Repository.Interface;
using CodeFirstApproachCRUD.Service.Interface;

namespace CodeFirstApproachCRUD.Service.Implementation
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task<User> AddUser(User User)
        {
            return await userRepository.AddUser(User);
        }

        public async Task<User> DeleteUser(int id)
        {
            return await userRepository.DeleteUser(id);
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await userRepository.GetAll(); 
        }

        public async Task<User> GetUser(int UserId)
        {
           return await userRepository.GetUser(UserId);
        }

        public async Task<User> UpdateUser(int UserId, User User)
        {
            return await userRepository.UpdateUser(UserId, User);
        }
    }
}
