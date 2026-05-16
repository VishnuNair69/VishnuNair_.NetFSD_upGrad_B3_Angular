using CartService.Models;
namespace CartService.Repositories
{
    public interface ICartRepository
    {
        Task<IEnumerable<CartItem>> GetByUserIdAsync(int userId);
        Task<CartItem?> GetItemAsync(int userId, int productId);
        Task<CartItem> AddOrUpdateAsync(CartItem item);
        Task<bool> RemoveAsync(int cartItemId);
        Task ClearCartAsync(int userId);
    }
}
