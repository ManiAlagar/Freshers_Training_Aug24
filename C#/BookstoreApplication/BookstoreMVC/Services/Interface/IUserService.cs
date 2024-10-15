using BookstoreMVC.Models;

namespace BookstoreMVC.Services.Interface
{
    public interface IUserService
    {
        Task<string> Login(User user);
        Task<string> Register(User user);
    }
}
