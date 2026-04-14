using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Data
{
    public class ApplicationDbContext : DbContext
    {
        // Constructor — receives DbContextOptions from DI and passes to base
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        // ─── DbSets (represent tables) ──────────────────────────────────────────
        public DbSet<User> Users { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        // ─── Fluent API configuration ───────────────────────────────────────────
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // User → Orders (one-to-many)
            modelBuilder.Entity<Order>()
                .HasOne(o => o.User)
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Order → OrderItems (one-to-many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Order)
                .WithMany(o => o.OrderItems)
                .HasForeignKey(oi => oi.OrderId)
                .OnDelete(DeleteBehavior.Cascade);

            // Product → OrderItems (one-to-many)
            modelBuilder.Entity<OrderItem>()
                .HasOne(oi => oi.Product)
                .WithMany(p => p.OrderItems)
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Restrict); // Restrict so products can't be deleted if used in orders

            // Decimal precision for money columns
            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.Price)
                .HasColumnType("decimal(18,2)");

            // Seed some sample products
            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, Name = "Laptop Pro X", Description = "High-performance laptop", Price = 75000, ImageUrl = "/images/laptop.jpg", Stock = 15 },
                new Product { ProductId = 2, Name = "Wireless Headphones", Description = "Noise-cancelling headphones", Price = 3500, ImageUrl = "/images/headphones.jpg", Stock = 30 },
                new Product { ProductId = 3, Name = "USB-C Hub", Description = "7-in-1 multiport adapter", Price = 1800, ImageUrl = "/images/hub.jpg", Stock = 50 }
            );
        }
    }
}
