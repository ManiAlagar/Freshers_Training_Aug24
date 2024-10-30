

using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
