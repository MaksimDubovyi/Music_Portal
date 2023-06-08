using Microsoft.EntityFrameworkCore;
using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public class Repository_Genre: IRepository_Genre
    {
        private readonly Music_Portal_Context _context;

        public Repository_Genre(Music_Portal_Context context)
        {
            _context = context;
        }

        public async Task<Genre> CheckGenre(string Genre)
        {
            return await _context.Genres.FirstOrDefaultAsync(p => p.Genres == Genre);
        }
        public async Task<List<Genre>> GetGenreList()
        {
            return await _context.Genres.ToListAsync();
        }
        public async Task AddGenre(Genre genre)
        {
            _context.Add(genre);
            await _context.SaveChangesAsync();
        }
        public bool Check()
        {
            if (_context.Genres == null)
                return true;
            else return false;
        }
        public async Task<Genre> GetGenre(int id)
        {
            return await _context.Genres.FindAsync(id);
        }
        public async Task DeleteGenre(Genre genre)
        {
            _context.Genres.Remove(genre);
            await _context.SaveChangesAsync();
        }
        public async Task UpdateGenre(Genre genre)
        {
            try
            {
                _context.Update(genre);
            }
            catch (Exception ex) { }
            await _context.SaveChangesAsync();
        }
    }
}

