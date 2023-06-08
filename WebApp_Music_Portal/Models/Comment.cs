using System.ComponentModel.DataAnnotations;

namespace WebApp_Music_Portal.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public int Id_User { get; set; }
        public int Id_Music { get; set; }
        public string Text { get; set; }
        public DateTime MessageDate { get; set; }
        public User User { get; set; }
        public Music Music { get; set; }
    }
}
