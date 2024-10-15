using BookstoreApplication.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IRoleService
    {
        Task<IEnumerable<Role>> GetAllRoles();
    }
}
