using ASPNETMVCCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace ASPNETMVCCRUD.Data
{
    public class DemoDbContext : DbContext
    {
        public DemoDbContext(DbContextOptions options) : base(options)
        {

        }


        public DbSet<Employee> Employees { get; set; }

    }
}
