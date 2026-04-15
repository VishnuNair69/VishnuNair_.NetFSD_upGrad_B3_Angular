using Microsoft.EntityFrameworkCore;
using WebApplication15.Models;

namespace WebApplication15.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<UserInfo> Users { get; set; }
        public DbSet<Contact> Contacts { get; set; }
    }
}
