using BookstoreApplication.Models;

namespace BookstoreApplication.Service.Interface
{
    public interface IUserService
    {
        Task<int> Register(User User);
        Task<User> GetUserById(int Id);
        Task<int> UpdateUser(int UserId, User user);
    }
}
