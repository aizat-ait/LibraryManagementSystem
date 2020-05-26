using LibraryManagement.Common;
using LibraryManagement.Data;
using LibraryManagement.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryManagement.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly LibraryManagementDBContext _context;

        public UserController(LibraryManagementDBContext context)
        {
            _context = context;
        }

        // POST: api/User
        [AllowAnonymous]
        [HttpPost]
        public IActionResult AddUser([FromBody]UserViewModel u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Invalid data.");
            }

            var now = Utility.GetTurkeyCurrentDateTime();

           var user = new UserModel
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                CreatedAt = now
            };

            _context.Users.Add(user);
            _context.SaveChanges();

            return Ok(user);
        }

        // GET: api/User
        [HttpGet]
        public ActionResult<IEnumerable<UserViewModel>> GetUsers()
        {
            var users = _context.Users.Select(u => new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                CreatedAt = u.CreatedAt
            }).ToList();

            if (users == null)
            {
                return NotFound();
            }

            return Ok(users);
        }

        // GET: api/User/:id
        [HttpGet("{id}")]
        public IActionResult GetUser(int id)
        {

            if (id <= 0)
            {
                return BadRequest();
            }

            var u = GetById(id);

            if (u == null)
            {
                return NotFound();
            }

            var user = new UserViewModel
            {
                Id = u.Id,
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Password = u.Password,
                CreatedAt = u.CreatedAt
            };

            return Ok(user);
        }

        [HttpPost("Edit")]
        public IActionResult Edit([FromBody]EditUserNameModel u)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Not a valid model");
            }

            var existing = GetById(u.Id);

            if (existing != null)
            {
                existing.Id = u.Id;
                existing.FirstName = u.FirstName;
                existing.LastName = u.LastName;

                _context.SaveChanges();
            }
            else
            {
                return NotFound();
            }

            return Ok();
        }

        // DELETE: api/User/:id
        [HttpDelete("{id}")]
        public IActionResult DeleteApplicationUserModel(int id)
        {
            if (id <= 0)
            {
                return BadRequest("");
            }

            var user = GetById(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            _context.SaveChanges();

            return Ok();
        }

        private UserModel GetById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }
    }
}
