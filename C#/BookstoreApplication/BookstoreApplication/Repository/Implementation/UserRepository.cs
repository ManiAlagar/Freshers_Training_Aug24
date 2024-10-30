using BookstoreApplication.Context;
using BookstoreApplication.Models;
using BookstoreApplication.Repository.Interface;
using System.Net;

namespace BookstoreApplication.Repository.Implementation
{
    public class UserRepository:IUserRepository
    {
        public readonly BookDBContext db;
        public UserRepository(BookDBContext context)
        {
            db = context;
        }
        public async Task<User> Register(User User)
        {
            try
            {
                
                var newUser = new User()
                {
                    FirstName = User.FirstName,
                    LastName = User.LastName,
                    Username = User.Username,
                    Email = User.Email,
                    Password = User.Password,
                    Address = User.Address,
                    ContactNo = User.ContactNo,
                    RoleId = User.RoleId
                };
                await db.Users.AddAsync(newUser);
                await db.SaveChangesAsync();
                return newUser;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<User?> GetUserById(int Id)
        {
            var user = await db.Users.FindAsync(Id);
            return user;
        }

    }
}

