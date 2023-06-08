using Microsoft.EntityFrameworkCore;
using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public class Repository_User : IRepository_User
    {
        private readonly Music_Portal_Context _context;

         public Repository_User(Music_Portal_Context context)
         {
            _context = context;
         }

        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FindAsync(id);
        }
        public async Task<User> GetUser(User users)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Name == users.Name && p.Password == users.Password);
        }
        public async Task<User> CheckName(string users)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Name == users);
        }
        public async Task<User> CheckEmail(string users)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Email == users);
        }
        public async Task AddUser(User user)
        {
            _context.Add(user);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateUser(User user)
        {
            try
            {
                _context.Update(user);
            }catch(Exception ex) { }
            await _context.SaveChangesAsync();
        }
        public async Task DeleteUser(User user)
        {
           _context.Users.Remove(user);
           await _context.SaveChangesAsync();
        }
        public bool Check()
        {
            if(_context.Users == null)
                return true;
            else return false;
        }

    }
    
}
