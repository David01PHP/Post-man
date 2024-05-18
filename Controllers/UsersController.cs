using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Collections.Data;
using Collections.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Collections.Controllers
{
    [Route("[controller]")]
    public class UsersController : Controller
    {
        public readonly UsersContext _context;

        public UsersController(UsersContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUser()
        {

            return _context.Users.ToList();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUserId(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser([FromBody]  User user)
        {
            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("PostUser", new { id = user.Id }, user);

        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<User>> DeleteUserId(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (User == null)
            {
                return NotFound();
            }
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        

        [HttpPut("{id}")]
        public ActionResult PutUserId(int id,[FromBody] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            var UserExistente = _context.Users.FirstOrDefault(n => n.Id == id);
            if (UserExistente == null)
            {
                return NotFound();
            }

            UserExistente.Name = user.Name;
            UserExistente.Last_Name = user.Last_Name;

            _context.SaveChanges();

            return NoContent();
        }


    }
}