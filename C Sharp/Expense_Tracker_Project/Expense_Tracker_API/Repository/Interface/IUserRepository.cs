using Expense_Tracker_API.Entity;


namespace Expense_Tracker_API.Repository.Interface
{
    public interface IUserRepository
    {
        Task<IEnumerable<Users>> Get();

        Task<Users> Get(int id);

        Task Add(Users entity);

        Task Edit(Users entity);
    }
}
