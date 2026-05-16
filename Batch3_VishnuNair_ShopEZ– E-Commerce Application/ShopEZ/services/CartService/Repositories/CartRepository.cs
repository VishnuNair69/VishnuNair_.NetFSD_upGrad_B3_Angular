using CartService.Data;
using CartService.Models;
using Microsoft.EntityFrameworkCore;
namespace CartService.Repositories
{
    public class CartRepository : ICartRepository
    {
        private readonly CartDbContext _context;
        public CartRepository(CartDbContext context) { _context = context; }
        public async Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId) => await _context.CartItems.Where(c => c.UserId == userId).ToListAsync();
        public async Task<CartItem?> GetItemAsync(int userId, int productId) => await _context.CartItems.FirstOrDefaultAsync(c => c.UserId == userId && c.ProductId == productId);
        public async Task<CartItem> AddOrUpdateAsync(CartItem item)
        {
            var existing = await GetItemAsync(item.UserId, item.ProductId);
            if (existing != null) { existing.Quantity += item.Quantity; await _context.SaveChangesAsync(); return existing; }
            _context.CartItems.Add(item); await _context.SaveChangesAsync(); return item;
        }
        public async Task<bool> RemoveAsync(int cartItemId)
        {
            var item = await _context.CartItems.FindAsync(cartItemId);
            if (item == null) return false;
            _context.CartItems.Remove(item); await _context.SaveChangesAsync(); return true;
        }
        public async Task ClearCartAsync(int userId)
        {
            var items = _context.CartItems.Where(c => c.UserId == userId);
            _context.CartItems.RemoveRange(items); await _context.SaveChangesAsync();
        }
    }
}
