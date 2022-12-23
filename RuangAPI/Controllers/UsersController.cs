using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
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
            UserParse usr = new UserParse()
            {
                id = result?.id.ToString(),
                username = result?.username,
                password = result?.password,
                email = result?.email,
                fullname = result?.fullname,
                nim = result?.nim,
            };
            if (result != null) return Ok(usr);
            else return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<UserParse[]>> Get()
        {
            UserParse[] UsersParses = new UserParse[_context.Users.Count()];
            var i = 0;
            foreach(var result in _context.Users)
            {
                
                UserParse usr = new UserParse()
                {
                    id = result?.id.ToString(),
                    username = result?.username,
                    password = result?.password,
                    email = result?.email,
                    fullname = result?.fullname,
                    nim = result?.nim,
                };
                UsersParses[i] = usr;
                i++;
            }
            return UsersParses;
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
