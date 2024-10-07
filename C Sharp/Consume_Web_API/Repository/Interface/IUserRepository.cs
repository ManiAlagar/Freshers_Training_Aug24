using DB_First_Approach.Entity;

namespace DB_First_Approach.Services.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> Get();
        Task<Users> Get(long id);
        Task Add(Users entity);
        Task Edit(Users dbEntity, Users entity);
        Task Delete(Users entity);
    }
}
