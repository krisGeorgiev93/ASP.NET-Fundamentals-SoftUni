using Microsoft.EntityFrameworkCore;
using RunGroopApp.Models;

namespace RunGroopApp.Data
{
	public class AppDbContext : DbContext
	{

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Club> Clubs { get; set; }

        public DbSet<User> Users { get; set; }

        public DbSet<Address> Addresses { get; set; }

        public DbSet<Race> Races { get; set; }
    }
}
