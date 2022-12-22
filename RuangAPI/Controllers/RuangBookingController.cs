using Microsoft.AspNetCore.Mvc;
using RuangAPI.Data;
using RuangAPI.Model;


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
        public JsonResult CreateEdit(RuangBooking booking)
        {
            if (booking.Id == 0)
            {
                _context.Bookings.Add(booking);
            }
            else
            {
                var bookingInDb = _context.Bookings.Find(booking.Id);

                if (bookingInDb == null) return new JsonResult(NotFound());
                bookingInDb = booking;
            }

            _context.SaveChanges();
            return new JsonResult(Ok(booking));
        }

        //Get 
        [HttpGet]
        public JsonResult Get(int id)
        {
            var result = _context.Bookings.Find(id);

            if (result == null) return new JsonResult(NotFound());
            return new JsonResult(Ok(result));
        }

        //Delete
        [HttpDelete]
        public JsonResult Delete(int id)
        {
            var result = _context.Bookings.Find(id);
            if (result == null)
                return new JsonResult(NotFound());

            _context.Bookings.Remove(result);
            _context.SaveChanges();

            return new JsonResult(NoContent());
        }

        //Get All
        [HttpGet()]
        public JsonResult GetAll()
        {
            var result = _context.Bookings.ToList();
            return new JsonResult(Ok(result));
        }
    }
}
