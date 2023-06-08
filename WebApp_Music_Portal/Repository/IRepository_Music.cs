using WebApp_Music_Portal.Models;

namespace WebApp_Music_Portal.Repository
{
    public interface IRepository_Music
    {
        Task<Music> GetMusic(int id);
        Task<User> GetUser(int id);
        Task<List<Music>> GetMusicList();
        Task<List<Music>> Get_Search_MusicList(string Search_);
        Task<List<Music>> Get_Genres_Search_MusicList(string Search_);
        Task<List<Genre>> GetGenreList();
        bool Check();
        Task<List<Comment>> GetComment(int id);
        Task<List<User>> GetUserList();
        Task AddComment(Comment comment);
        Task AddMusic(Music music);
        Task DeleteMusic(Music music);
        Task UpdateMusic(Music music);
    }
}
