using Microsoft.AspNetCore.Mvc;
using RuangAPI.Data;
using RuangAPI.Model;
using System.Diagnostics;

namespace RuangAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RuangBookingController : ControllerBase
    {
        private readonly APIContext _context;
        public RuangBookingController(APIContext context)
        {
            _context = context;
        }

        //Create/Edit
        [HttpPost]
        public async Task<ActionResult<RuangBooking>> Create(RuangBooking Bookings)
        {
            Debug.WriteLine(Bookings);
            try
            {
                await _context.RuangBooking.AddAsync(Bookings);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Update(RuangBooking Bookings)
        {
            _context.RuangBooking.Update(Bookings);
            await _context.SaveChangesAsync();
            return Ok();
        }

        //Get 
        [HttpGet("{id:int}")]
        public async Task<ActionResult<RuangBooking>> GetById(int id)
        {
            var result = await _context.RuangBooking.FindAsync(id);
            if (result != null) return Ok(result);
            else return NotFound();
        }

        [HttpGet]
        public ActionResult<IEnumerable<RuangBooking>> Get()
        {
            return _context.RuangBooking;
        }

        //Delete
        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var result = await _context.RuangBooking.FindAsync(id);
            if (result != null) _context.RuangBooking.Remove(result);
            else return NotFound();
            await _context.SaveChangesAsync();
            return Ok();
        }
    }
}
