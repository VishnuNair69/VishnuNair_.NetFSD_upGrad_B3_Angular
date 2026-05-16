using Microsoft.EntityFrameworkCore;
using OrderService.Models;
namespace OrderService.Data
{
    public class OrderDbContext : DbContext
    {
        public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        protected override void OnModelCreating(ModelBuilder mb)
        {
            mb.Entity<Order>().Property(o => o.TotalAmount).HasColumnType("decimal(18,2)");
            mb.Entity<OrderItem>().Property(oi => oi.Price).HasColumnType("decimal(18,2)");
            mb.Entity<OrderItem>().HasOne(oi => oi.Order).WithMany(o => o.OrderItems).HasForeignKey(oi => oi.OrderId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
