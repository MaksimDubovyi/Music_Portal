using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace WebApp_Music_Portal.Models
{
    public class Genre
    {
        public int Id { get; set; }
        [Display(Name = "Жанр")]
        [Remote(action: "CheckGenre", controller: "Genres", ErrorMessage = "Жанр вже існує!")]
        [Required(ErrorMessage = "Поле має бути встановлене")]
        public string Genres { get; set; }
    }
}
