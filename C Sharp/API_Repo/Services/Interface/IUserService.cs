using CRUD_Web_API.Entity;

namespace CRUD_Web_API.Services.Interface
{
    public interface IUserService
    {
        IEnumerable<Users> GetAll();
        Users Get(long id);
        void Add(Users entity);
        void Edit(Users dbEntity, Users entity);
        void Delete(Users entity);
    }
}
