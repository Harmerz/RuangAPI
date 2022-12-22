using Microsoft.AspNetCore.Mvc;
using RuangAPI.Data;
using RuangAPI.Model;
using System.Diagnostics;

namespace RuangAPI.Controllers
{
    [Route("/auth/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly APIContext _context;

        public LoginController(APIContext context)
        {
            _context = context;
        }

        //Create/Edit
        [HttpPost]
        public async Task<ActionResult<Login>> Create(Login Login)
        {
            Debug.WriteLine(Login);
            try
            {
                await _context.Login.AddAsync(Login);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Ok();
        }

        //Get 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<Login>> GetById(int id)
        {
            var result = await _context.Login.FindAsync(id);
            if (result != null) return Ok(result);
            else return NotFound();
        }
    }
}
