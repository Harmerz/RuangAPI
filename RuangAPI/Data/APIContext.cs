using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using RuangAPI.Model;
using System.Diagnostics;

namespace RuangAPI.Data
{
    public class APIContext : DbContext
    {
        public DbSet<RuangBooking> RuangBooking { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Login> Login { get; set; }
        public APIContext(DbContextOptions<APIContext> options)
            : base(options)
        {
            try
            {
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
                if(databaseCreator != null)
                {
                    // Create Databasse if cannot Connect
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();

                    // Create Tables if no table exist
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
