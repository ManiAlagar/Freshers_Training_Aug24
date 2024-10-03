using CRUD_Web_API.Entity;
using CRUD_Web_API.Entity.DBContext;
using CRUD_Web_API.Repository.Interface;

namespace CRUD_Web_API.Repository.Implementation
{
    public class UserRepository : IUserRepository
    {
        private readonly UserContext context;
        public UserRepository(UserContext context)
        {
            this.context = context;
        }

        public IEnumerable<Users> GetAll()
        {
            return context.User.ToList();
        }

        public Users Get(long id)
        {
            return context.User.FirstOrDefault(e => e.ID == id);
        }

        public void Add(Users user)
        {
            context.User.Add(user);
            context.SaveChanges();
        }


        public void Edit(Users user, Users entity)
        {
            user.UserName = entity.UserName;
            user.Password = entity.Password;
            user.Email = entity.Email;
            user.PhoneNumber = entity.PhoneNumber;

            context.Update(user);
            context.SaveChanges();
        }

        public void Delete(Users user)
        {
            context.User.Remove(user);
            context.SaveChanges();
        }
    }
}
