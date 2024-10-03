namespace CodeFirstApproachCRUD.Service.Interface
{
    public interface IUserService 
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetUser(int UserId);
        Task<User> AddUser(User User);
        Task<User> UpdateUser(int UserId, User User);
        Task<User> DeleteUser(int id);
    }
}
