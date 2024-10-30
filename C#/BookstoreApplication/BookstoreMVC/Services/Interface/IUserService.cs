using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IUserService
    {
        Task<string> Login(LoginModel user);
        Task<string> Register(User user);
    }
}
