
using System.ComponentModel.DataAnnotations;

namespace WebApp_Music_Portal.Models
{
    public class User
    {
        public User()
        {
            //this.Musics = new HashSet<Music>();
        }

        public int Id { get; set; }
        [Required(ErrorMessage = "Поле має бути встановлене")]
        [Display(Name = "Ім'я")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Поле має бути встановлене")]
        [Display(Name = "Пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required(ErrorMessage = "Поле має бути встановлене")]
        [Display(Name = "Електронна пошта")]
        public string? Email { get; set; }
        [Required(ErrorMessage = "Поле має бути встановлене")]
        [Display(Name = "Вік")]
        public int Age { get; set; }
        [Range(2, 3, ErrorMessage = "Можливо 2 або 3!")]
        public int? Admin { get; set; }
        //public ICollection<Music> Musics { get; set; }
    }
}
