using Microsoft.AspNetCore.Mvc;
using RuangAPI.Data;
using RuangAPI.Model;
using System.Diagnostics;

namespace RuangAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly APIContext _context;

        public UsersController(APIContext context)
        {
            _context = context;
        }

        //Create/Edit
        [HttpPost]
        public async Task<ActionResult<Users>> Create(Users users)
        {
            Debug.WriteLine(users);
            try
            {
                await _context.Users.AddAsync(users);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(Users users)
        {
            _context.Users.Update(users);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Get 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Users>> GetById(int id)
        {
            var result = await _context.Users.FindAsync(id);
            if (result != null) return Ok(result);
            else return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<Users>> Get()
        {
            return _context.Users;
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _context.Users.FindAsync(id);
            if (result != null) _context.Users.Remove(result);
            else return NotFound();
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
