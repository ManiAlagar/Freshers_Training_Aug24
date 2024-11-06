using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IUserService
    {
        Task<string> Login(LoginModel user);
        Task<int> Register(User user);
        Task<User> GetUserById(int Id, string? token);
        Task<int> UpdateUser(int id, User user, string? token);
    }
}
