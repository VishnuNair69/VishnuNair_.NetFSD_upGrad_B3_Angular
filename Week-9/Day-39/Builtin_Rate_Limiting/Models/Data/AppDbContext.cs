using Microsoft.EntityFrameworkCore;

namespace WebApplication18.Models.Data
{
    public class AppDbContext :DbContext
    {
         public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
            {
            }

            public DbSet<Contact> Contacts { get; set; }
    }
}
