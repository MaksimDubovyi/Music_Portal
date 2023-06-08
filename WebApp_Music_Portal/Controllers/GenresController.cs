using Microsoft.AspNetCore.Mvc;
using WebApp_Music_Portal.Models;
using WebApp_Music_Portal.Repository;

namespace WebApp_Music_Portal.Controllers
{
    [ApiController]
    [Route("api/Genres")]
    public class GenresController : ControllerBase
    {
        IRepository_Genre Repository;

        public GenresController(IRepository_Genre r)
        {
            Repository = r;
        }


        private async Task<bool> CheckGenre(string Genres)
        {
            var x = await Repository.CheckGenre(Genres);
            if (x != null)
                return false;
            return true;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new ObjectResult(await Repository.GetGenreList());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || Repository.Check())
                return NotFound();

            var genr = await Repository.GetGenre(id);
            if (genr == null)
            {
                return NotFound();
            }
            return new ObjectResult(genr);
        }

        [HttpPost]
        public async Task<IActionResult> Create(Genre genr)
        {
            if (!await CheckGenre(genr.Genres))
                return BadRequest("gg");
            if (ModelState.IsValid)
            {
                await Repository.AddGenre(genr);
                return Ok(genr);
            }
            return BadRequest(ModelState);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (Repository.Check())
            {
                return Problem("Entity set 'Music_Portal_Context.Genres'  is null.");
            }
            var genre = await Repository.GetGenre(id); 
            if (genre != null)
            {
                await Repository.DeleteGenre(genre);
            }
            return Ok(genre);
        }
        [HttpPut]
        public async Task<IActionResult> Edit(Genre genr)
        {
            if (!await CheckGenre(genr.Genres))
                return BadRequest("gg");
            var genre = await Repository.GetGenre(genr.Id);
            if (genre == null)
                return NotFound();
            genre.Genres = genr.Genres;
            await Repository.UpdateGenre(genre);
            return Ok(genre);
        }
    }
}
