using Microsoft.EntityFrameworkCore;
using WebApplication14.Models;
namespace WebApplication14.Data
{
    public class AppDbContext :DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Contact> Contacts { get; set; }
    }
}
