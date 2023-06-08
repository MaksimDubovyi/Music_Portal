using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public interface IRepository_Genre
    {
        Task<Genre> CheckGenre(string Genre);
        Task<List<Genre>> GetGenreList();
        Task AddGenre(Genre genre);
        bool Check();
        Task<Genre> GetGenre(int id);
        Task DeleteGenre(Genre genre);
        Task UpdateGenre(Genre genre);
    }
}
