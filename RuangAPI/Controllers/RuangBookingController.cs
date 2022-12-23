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
            RuangBookingParse ruangBookingParse = new RuangBookingParse()
            {
                bookId = result?.bookId.ToString(),
                name = result?.name,
                room = result?.room,
                nim = result?.nim,
                date = result?.date,
                _00 = result?._00,
                _01 = result?._01,
                _02 = result?._02,
                purpose = result?.purpose,
                person = result?.person,

            };
            if (result != null) return Ok(ruangBookingParse);
            else return NotFound();
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<RuangBookingParse>>> GetByDateAndRoom(string? date, string? room)
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
            var temp = await result.ToListAsync();
            RuangBookingParse[] ruangBookingParses = new RuangBookingParse[temp.Count()];
            var i = 0;
            foreach (var rooms in temp)
            {

                RuangBookingParse ruangBookingParse = new RuangBookingParse()
                {
                    bookId = rooms?.bookId.ToString(),
                    name = rooms?.name,
                    room = rooms?.room,
                    nim = rooms?.nim,
                    date = rooms?.date,
                    _00 = rooms?._00,
                    _01 = rooms?._01,
                    _02 = rooms?._02,
                    purpose = rooms?.purpose,
                    person = rooms?.person,

                };
                ruangBookingParses[i] = ruangBookingParse;
                i++;
            }
            return ruangBookingParses;
        }

        [HttpGet("/GetAllRuangBooking")]
        public ActionResult<IEnumerable<RuangBookingParse>> Get()
        {
            RuangBookingParse[] ruangBookingParses = new RuangBookingParse[_context.RuangBooking.Count()];
            var i = 0;
            foreach (var rooms in _context.RuangBooking)
            {

                RuangBookingParse ruangBookingParse = new RuangBookingParse()
                {
                    bookId = rooms?.bookId.ToString(),
                    name = rooms?.name,
                    room = rooms?.room,
                    nim = rooms?.nim,
                    date = rooms?.date,
                    _00 = rooms?._00,
                    _01 = rooms?._01,
                    _02 = rooms?._02,
                    purpose = rooms?.purpose,
                    person = rooms?.person,

                };
                ruangBookingParses[i] = ruangBookingParse;
                i++;
            }
            return ruangBookingParses;
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
