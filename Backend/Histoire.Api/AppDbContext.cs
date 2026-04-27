using Histoire.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Histoire.Api
{
    public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
    {
        public DbSet<Person> Persons { get; set; }
        public DbSet<Event> Events { get; set; }
    }
}
