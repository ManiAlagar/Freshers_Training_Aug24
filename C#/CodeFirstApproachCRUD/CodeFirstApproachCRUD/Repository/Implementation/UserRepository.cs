using CodeFirstApproachCRUD.Context;
using CodeFirstApproachCRUD.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace CodeFirstApproachCRUD.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        public readonly UserDbContext db;
        
        public UserRepository(UserDbContext context)
        {
            db = context;
        }
        public async Task<User> AddUser(User User)
        {
            var newUser = new User()
            {
                UserName = User.UserName,
                UserEmail = User.UserEmail,
                UserPhone = User.UserPhone,
                UserPassword = User.UserPassword

            };
            await db.Users.AddAsync(newUser);
            await db.SaveChangesAsync();
            return newUser;
        }

        public async Task<User> DeleteUser(int id)
        {
            var DeleteUser = await db.Users.FindAsync(id);
            if (DeleteUser != null)
            {
                db.Remove(DeleteUser);
                await db.SaveChangesAsync();

            }
            return DeleteUser;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await db.Users.ToListAsync();
        }

        public async Task<User> GetUser(int UserId)
        {
            var user = await db.Users.FindAsync(UserId);
            return user;
        }

        public async Task<User> UpdateUser(int UserId, User User)
        {
            var existingUser = await db.Users.FindAsync(UserId);
            if (existingUser != null)
            {
                existingUser.UserName = User.UserName;
                existingUser.UserEmail = User.UserEmail;
                existingUser.UserPhone = User.UserPhone;
                existingUser.UserPassword = User.UserPassword;
                await db.SaveChangesAsync();
                return existingUser;
            }
            return null;
        }
    }
}
