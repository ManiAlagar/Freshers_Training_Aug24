using Expense_Tracker_MVC.Models;

namespace Expense_Tracker_MVC.Service.Interface
{
    public interface IUserService
    {
        Task<string> Login(Login credential);

        Task<Users> Get(int? id);


        Task<bool> Create(Users entity);

        Task<bool> Edit(Users entity);
    }
}
