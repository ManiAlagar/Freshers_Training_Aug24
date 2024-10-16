using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IUserRepository
    {
         Task<User> Register(User User);
        Task<User> GetUserById(int Id);
    }
}
