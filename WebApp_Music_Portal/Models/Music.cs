using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using static Azure.Core.HttpHeader;

namespace WebApp_Music_Portal.Models
{
    public class Music
    {
        public Music()
        {
            //this.Comments = new HashSet<Comment>();
        }
        public int Id { get; set; }
        public int Id_User { get; set; }
        public string Name { get; set; }
        public string? Path { get; set; }
        public string? Executor { get; set; }
        public string Size { get; set; }
        public float? GPA { get; set; }
        public string Genres { get; set; }
        //public ICollection<Comment>? Comments { get; set; }
    }
}
