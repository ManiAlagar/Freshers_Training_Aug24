using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
