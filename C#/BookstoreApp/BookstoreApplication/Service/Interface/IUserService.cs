using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IUserService
    {
        Task<User> Register(User User);
        Task<User> GetUserById(int Id);
    }
}
