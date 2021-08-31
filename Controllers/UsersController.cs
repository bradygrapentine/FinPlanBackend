using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using FinPlanBackend.Models;

namespace FinPlanBackend.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly DatabaseContext _context;

        public UsersController(DatabaseContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            var user = await _context.User.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user, string choice)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }

            if (choice == "college")
            {
                user.Choice = "college";
                user.Debt = 50000;
            }
            else if (choice == "trade")
            {
                user.Choice = "trade";
                user.Debt = 10000;
            }
            else if (choice == "job")
            {
                user.Choice = "job";
                user.Cash = 50000;
            }
            else
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _context.User.Add(user);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (UserExists(user.Id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        private bool UserExists(int id)
        {
            return _context.User.Any(user => user.Id == id);
        }
    }
}


// [HttpDelete("{id}")]
// public async Task<IActionResult> DeleteUser(string id)
// {
//     var user = await _context.User.FindAsync(id);
//     if (user == null)
//     {
//         return NotFound();
//     }

//     _context.User.Remove(user);

//     await _context.SaveChangesAsync();

//     return Ok(user);
// }

// [HttpGet]
// public async Task<ActionResult<IEnumerable<User>>> GetUsers()
// {

//     return await _context.User.OrderBy(row => row.Id).ToListAsync();
// }