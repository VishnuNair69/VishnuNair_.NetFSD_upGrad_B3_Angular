using Microsoft.EntityFrameworkCore;
using OrderService.Data;
using OrderService.Models;
namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext context) { _context = context; }
        public async Task<IEnumerable<Order>> GetAllAsync() => await _context.Orders.Include(o => o.OrderItems).ToListAsync();
        public async Task<Order?> GetByIdAsync(int id) => await _context.Orders.Include(o => o.OrderItems).FirstOrDefaultAsync(o => o.OrderId == id);
        public async Task<Order> AddAsync(Order order) { _context.Orders.Add(order); await _context.SaveChangesAsync(); return order; }
    }
}
