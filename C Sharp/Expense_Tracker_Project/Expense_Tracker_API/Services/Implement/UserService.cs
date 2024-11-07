using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Repository.Interface;
using Expense_Tracker_API.Services.Interface;

namespace Expense_Tracker_API.Services.Implement
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;
        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }


        public async Task<IEnumerable<Users>> Get()
        {
            return await userRepository.Get();
        }

        public async Task<Users> Get(int id)
        {
        return await userRepository.Get(id);
        }

        public async Task<bool> Add(Users entity)
        {
            return await userRepository.Add(entity);
        }

        public async Task<bool> Edit(Users entity)
        {
            return await userRepository.Edit(entity);
        }


    }
}
