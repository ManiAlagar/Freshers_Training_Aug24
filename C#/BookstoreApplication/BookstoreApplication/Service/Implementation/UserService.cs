using BookstoreApplication.Models;
using BookstoreApplication.Repository.Implementation;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class UserService :IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }
        public async Task<int> Register(User User)
        {
            return await userRepository.Register(User);
        }
        public async Task<User> GetUserById(int Id)
        {
            return await userRepository.GetUserById(Id);
        }
        public async Task<int> UpdateUser(int UserId, User user)
        {
            return await userRepository.UpdateUser(UserId, user);
        }
    }
}
