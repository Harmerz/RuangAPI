using Microsoft.EntityFrameworkCore;
using RuangAPI.Model;

namespace RuangAPI.Data
{
    public class APIContext : DbContext
    {
        public DbSet<RuangBooking> Bookings { get; set; }
        public APIContext(DbContextOptions<APIContext> options)
            :base (options)
        {

        }
    }
}
