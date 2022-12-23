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
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LoginParse>>> GetByUsrPass(string username, string password)
        {
            var result = _context.Login.AsQueryable();
            if (username != null)
            {
                result = result.Where(entry => entry.username == username);
            }
            if (password != null)
            {
                result = result.Where(entry => entry.password == password);
            }
            var temp = await result.ToListAsync();
            LoginParse[] loginParses = new LoginParse[temp.Count()];
            var i = 0;
            foreach (var login in temp)
            {

                LoginParse loginParse = new LoginParse()
                {
                    id = login?.id.ToString(),
                    username = login?.username,
                    password = login?.password,

                };
                loginParses[i] = loginParse;
                i++;
            }
            return loginParses;
        }
    }
}
