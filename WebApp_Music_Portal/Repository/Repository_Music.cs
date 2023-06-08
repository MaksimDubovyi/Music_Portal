using Microsoft.EntityFrameworkCore;
using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public class Repository_Music: IRepository_Music
    {
        private readonly Music_Portal_Context _context;

        public Repository_Music(Music_Portal_Context context)
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
            return await  _context.Musics.ToListAsync();
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

        public async Task<Music> GetMusic(int id)
        {
            return await _context.Musics.FirstOrDefaultAsync(m => m.Id == id);
                      
        }
        public async Task<List<Comment>> GetComment(int id)
        {
            return await _context.Comments.Where(p => p.Music.Id == id).ToListAsync();
        }
        public async Task<List<User>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }
        public async Task<User> GetUser(int id)
        {
            return await _context.Users.FirstOrDefaultAsync(p => p.Id == id);
        }

        public async Task AddComment(Comment comment)
        {
            _context.Add(comment);
            await _context.SaveChangesAsync();
        }
        public async Task AddMusic(Music music)
        {
            _context.Add(music);
            await _context.SaveChangesAsync();
        }
        public async Task DeleteMusic(Music music)
        {
            _context.Musics.Remove(music);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateMusic(Music music)
        {
            _context.Update(music);
            await _context.SaveChangesAsync();
        }

    }

    





}
