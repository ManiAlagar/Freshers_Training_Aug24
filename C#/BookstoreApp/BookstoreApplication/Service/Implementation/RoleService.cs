using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using BookstoreApplication.Service.Interface;

namespace BookstoreApplication.Service.Implementation
{
    public class RoleService : IRoleService
    {
        private readonly IRoleRepository roleRepository;
        public RoleService(IRoleRepository roleRepository) 
        { 
            this.roleRepository = roleRepository;   
        }
        public async Task<IEnumerable<Role>> GetAllRoles()
        {
            return await roleRepository.GetAllRoles();
        }
    }
}
