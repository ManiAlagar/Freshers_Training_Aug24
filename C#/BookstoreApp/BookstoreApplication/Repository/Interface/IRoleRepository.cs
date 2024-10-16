using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IRoleRepository
    {
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
