using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public interface IRepository_User
    {
        Task<User> GetUser(int id);
        Task<User> GetUser(User users);
        Task<List<User>> GetUserList();
        Task<User> CheckName(string users);
        Task<User> CheckEmail(string users);
        Task AddUser(User users);
        Task UpdateUser(User users);
        Task DeleteUser(User users);
        bool Check();
        
    }

}

