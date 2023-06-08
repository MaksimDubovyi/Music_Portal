using Microsoft.EntityFrameworkCore;
using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public class Repository_Admin: IRepository_Admin
    {
        private readonly Music_Portal_Context _context;
        public Repository_Admin(Music_Portal_Context context)
        {
            _context = context;
        }
        public bool Check()
        {
            if (_context.Musics == null)
                return true;
            else return false;
        }
        public async Task<List<Music>> GetMusicList()
        {
            return await _context.Musics.ToListAsync();
        }
        public async Task<List<Genre>> GetGenreList()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task<List<Music>> Get_Search_MusicList(string Search_)
        {
            return await _context.Musics.Where(p => p.Name == Search_).ToListAsync();
        }
        public async Task<List<Music>> Get_Genres_Search_MusicList(string id)
        {
            return await _context.Musics.Where(p => p.Genres == id).ToListAsync();
        }
        public async Task<List<Music>> Get_GPA_Search_MusicList()
        {
            return await _context.Musics.OrderByDescending(p => p.GPA).ToListAsync();
        }
        public async Task<List<Music>> Get_My_Search_MusicList(int id)
        {
            return await _context.Musics.Where(p => p.Id_User == id).ToListAsync();
        }
    }
}
