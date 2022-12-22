using Microsoft.AspNetCore.Mvc;
using RuangAPI.Data;
using RuangAPI.Model;
using System.Diagnostics;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Xml.Linq;

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
        public async Task<ActionResult<IEnumerable<RuangBooking>>> GetByDateAndRoom(string date, string room)
        {
            var result = _context.RuangBooking.AsQueryable();
            if (date != null)
            {
                result = result.Where(entry => entry.date == date);
            }
            if (room != null)
            {
                result = result.Where(entry => entry.room == room);
            }

            return await result.ToListAsync();
        }

        [HttpGet("/GetAllRuangBooking")]
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
