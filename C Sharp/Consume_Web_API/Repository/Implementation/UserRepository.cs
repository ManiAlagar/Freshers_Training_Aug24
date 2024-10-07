using DB_First_Approach.Entity;
using DB_First_Approach.Entity.DBContext;
using DB_First_Approach.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace DB_First_Approach.Repository.Implementation
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
            return await context.User.ToListAsync();
        }

        public async Task<Users> Get(long id)
        {
            return await context.User.FirstOrDefaultAsync(e => e.ID == id);
        }

        public async Task Add(Users user)
        { 
            await  context.User.AddAsync(user);
            context.SaveChangesAsync();
        }


        public async Task Edit(Users user, Users entity)
        {
            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.Email = entity.Email;
            user.PhoneNumber = entity.PhoneNumber;

            context.User.Update(user);
            await context.SaveChangesAsync();
        }

        public async Task Delete(Users user)
        {
             context.User.Remove(user);
            await context.SaveChangesAsync();
        }

    }
}
