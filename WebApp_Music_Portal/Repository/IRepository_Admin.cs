using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public interface IRepository_Admin
    {
        Task<List<Music>> GetMusicList();
        Task<List<Music>> Get_Search_MusicList(string Search_);
        Task<List<Music>> Get_Genres_Search_MusicList(string Search_);
        Task<List<Music>> Get_GPA_Search_MusicList();
        Task<List<Music>> Get_My_Search_MusicList(int id);
        Task<List<Genre>> GetGenreList(); 
        bool Check();
    }
}
