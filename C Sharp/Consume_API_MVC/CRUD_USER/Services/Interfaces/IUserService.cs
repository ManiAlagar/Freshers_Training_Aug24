using CRUD_USER.Models;

namespace Users_CRUD.Web.Services.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<Users>> Get();

        Task<Users> Get(int? id);

        Task Create(Users entity);

        Task Edit(Users entity);

        Task Delete(int id);
       
    }
}
