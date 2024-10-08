using DB_First_Approach.Entity;
using DB_First_Approach.Services.Interface;

namespace DB_First_Approach.Services.Implementation
{
    public class UserService : IUserService
    {
        private IUserRepository userRepository;

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

        public async Task  Add(Users entity)
        {
             await userRepository.Add(entity);
        }

        public async Task Edit(Users entity)
        {
            await userRepository.Edit(entity);
        }

        public async Task Delete(Users entity)
        {
           await  userRepository.Delete(entity);
        }
    }
}
