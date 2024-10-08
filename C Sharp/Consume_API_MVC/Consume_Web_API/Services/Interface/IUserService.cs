using DB_First_Approach.Entity;

namespace DB_First_Approach.Services.Interface
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> Get();
       
        Task<Users> Get(int id);
       
        Task Add(Users entity);
       
        Task Edit(Users entity);
       
        Task Delete(Users entity);
    }
}
