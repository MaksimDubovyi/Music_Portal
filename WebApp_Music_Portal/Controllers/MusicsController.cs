using Microsoft.AspNetCore.Mvc;
using WebApp_Music_Portal.Models;
using WebApp_Music_Portal.Repository;
using System.IO;
namespace WebApp_Music_Portal.Controllers
{
    [ApiController]
    [Route("api/Musics")]
    public class MusicsController : ControllerBase
    {
        IWebHostEnvironment _appEnvironment;
        IRepository_Music Repository;
        public MusicsController(Music_Portal_Context context, IWebHostEnvironment appEnvironment, IRepository_Music r)
        {
            Repository = r;
            _appEnvironment = appEnvironment;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new ObjectResult(await Repository.GetMusicList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {

            if (id == null || Repository.Check())
                return NotFound();

            Music Musucs = await Repository.GetMusic(id);
            if (Musucs == null)
            {
                return NotFound();
            }
            return new ObjectResult(Musucs);
        }

        [HttpPost]
        public async Task<ActionResult<Music>> Create([FromForm] MusicModel requestData)
        {
            IFormFile uploadedFilej = requestData.uploadedFilej;

            try
            {
                string path = Path.Combine(_appEnvironment.WebRootPath, "mp3", uploadedFilej.FileName);
                using var fileStream = new FileStream(path, FileMode.Create);
                await uploadedFilej.CopyToAsync(fileStream);
            }
            catch (FileNotFoundException) { }
            Music music = new Music
            {
                Id_User = 1,
                Size = (uploadedFilej.Length / 1000000).ToString() + "Mb",
                Path = uploadedFilej.FileName,
                Genres = requestData.myComboBoxj,
                Executor = requestData.Music_Executorj,
                Name = requestData.Music_Namej
            };
            if (ModelState.IsValid)
            {
                await Repository.AddMusic(music);
                return Ok(music);
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        
        [HttpPut]
        public async Task<IActionResult> Edit(MusicEdit music)
        {
            var musica = await Repository.GetMusic(music.Id);
            if (music == null)
                return NotFound();
            musica.Executor = music.Executor;
            musica.Name = music.Name;
            musica.Genres = music.Genres;
            await Repository.UpdateMusic(musica);
            return Ok(musica);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Repository.Check())
                return Problem("Entity set 'Music_Portal_Context.Users'  is null.");
            var music = await Repository.GetMusic(id);

            string filePath = Path.Combine(_appEnvironment.WebRootPath, "mp3", music.Path);
            if (music != null)
               await Repository.DeleteMusic(music);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
            return Ok(music);
        }
    }
}
