using CRUD_Web_API.Entity;

namespace CRUD_Web_API.Repository.Interface
{
    public interface IUserRepository
    {
        IEnumerable<Users> GetAll();
        Users Get(long id);
        void Add(Users entity);
        void Edit(Users dbEntity, Users entity);
        void Delete(Users entity);
    }
}
