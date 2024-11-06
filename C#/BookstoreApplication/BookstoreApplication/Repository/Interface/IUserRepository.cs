using BookstoreApplication.Models;

namespace BookstoreApplication.Repository.Interface
{
    public interface IUserRepository
    {
         Task<int> Register(User User);
        Task<User> GetUserById(int Id);
        Task<int> UpdateUser(int bookId, User user);
    }
}
