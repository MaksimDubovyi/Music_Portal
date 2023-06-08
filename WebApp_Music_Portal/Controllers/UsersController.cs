using Microsoft.AspNetCore.Mvc;
using WebApp_Music_Portal.Models;
using WebApp_Music_Portal.Repository;
using System.Text.RegularExpressions;

namespace WebApp_Music_Portal.Controllers
{
    [ApiController]
    [Route("api/Users")]
    public class UsersController : ControllerBase
    {
        IRepository_User Repository;
        public UsersController(Music_Portal_Context context, IRepository_User r)
        {
            Repository = r;
        }

        public bool IsValidEmail(string email)
        {
            string pattern = @"^[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}$";
            return Regex.IsMatch(email, pattern);
        }
        private async Task<bool> CheckName(string name)
        {
            var x = await Repository.CheckName(name);
            if (x != null)
                return false;
            return true;
        }
        private async Task<bool> CheckEmail(string email)
        {
            var x = await Repository.CheckEmail(email);
            if (x != null)
                return false;
            return true;
        }
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            return new ObjectResult(await Repository.GetUserList());
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Details(int id)
        {
            if (id == null || Repository.Check())
                return NotFound();

            var user = await Repository.GetUser(id);
            if (user == null)
                return NotFound();

            return new ObjectResult(user);
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostCreate([Bind("Name,Password,Email,Age")] User user)
        {
            if(!await CheckName(user.Name))
                return BadRequest("nn");
            if (!await CheckEmail(user.Email))
                return BadRequest("ee");
            if (!IsValidEmail(user.Email))
                return BadRequest("dd");

            if (ModelState.IsValid)
            {
                if (user.Name == "admin" && user.Password == "111111")
                    user.Admin = 1;
                else
                    user.Admin = 2;

                await Repository.AddUser(user);
                return Ok(user);
            }
            else
                return BadRequest(ModelState);
        }
        [HttpPut]
        public async Task<ActionResult<User>> PutEdit(User user)
        {
            if (!IsValidEmail(user.Email))
                return BadRequest("dd");
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var use = await Repository.GetUser(user.Id);
            if (use == null)
                return NotFound();

            use.Age = user.Age;
            use.Admin = user.Admin;
            use.Email = user.Email;
            use.Name = user.Name;
            use.Password = user.Password;
            await Repository.UpdateUser(use);
           return Ok(use);
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (Repository.Check())
                return Problem("Entity set 'Music_Portal_Context.Users'  is null.");
            var user = await Repository.GetUser(id);
            if (user != null)
               await Repository.DeleteUser(user);

            return Ok(user);
        }
    }
}
