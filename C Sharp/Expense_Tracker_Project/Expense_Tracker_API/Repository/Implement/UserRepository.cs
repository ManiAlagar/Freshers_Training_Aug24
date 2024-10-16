using Expense_Tracker_API.Entity;
using Expense_Tracker_API.Entity.DBContext;
using Expense_Tracker_API.Repository.Interface;
using Microsoft.EntityFrameworkCore;

namespace Expense_Tracker_API.Repository.Implement
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context; 
        }

        public async Task<IEnumerable<Users>> Get()
        {
            return await context.Users.ToListAsync();
        }

        public async Task<Users> Get(int id)
        {
            return await context.Users.FirstOrDefaultAsync(e => e.UserID == id);
        }

        public async Task<bool> Add(Users user)
        {

            Users User = await context.Users.FirstOrDefaultAsync(e => e.Email == user.Email || e.MobileNumber == user.MobileNumber);

            if (User == null)
            {
                await context.Users.AddAsync(user);
                await context.SaveChangesAsync();
                return true;
            }
            return false;
        }


        public async Task Edit(Users entity)
        {
            Users user = await Get(entity.UserID);

            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.Gender = entity.Gender;
            user.Email = entity.Email;
            user.MobileNumber = entity.MobileNumber;

            context.Users.Update(user);
            await context.SaveChangesAsync();
        }
    }
}
