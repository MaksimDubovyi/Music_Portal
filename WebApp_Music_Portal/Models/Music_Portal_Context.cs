using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
namespace WebApp_Music_Portal.Models
{
    public class Music_Portal_Context : DbContext
    {
        public Music_Portal_Context(DbContextOptions<Music_Portal_Context> options)
           : base(options)
        {
              Database.EnsureCreated();
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Music> Musics { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<Genre> Genres { get; set; }

    }
}
