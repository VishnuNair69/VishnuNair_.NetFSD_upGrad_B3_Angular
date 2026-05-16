using CartService.DTOs;
using CartService.Models;
using CartService.Repositories;
namespace CartService.Services
{
    public class CartServiceImpl : ICartService
    {
        private readonly ICartRepository _repo;
        public CartServiceImpl(ICartRepository repo) { _repo = repo; }

        public async Task<IEnumerable<CartItemDTO>> GetCartAsync(int userId) =>
            (await _repo.GetByUserIdAsync(userId)).Select(Map);

        public async Task<CartItemDTO> AddToCartAsync(AddToCartDTO dto)
        {
            if (dto.Quantity <= 0) throw new ArgumentException("Quantity must be > 0.");
            if (dto.Price <= 0) throw new ArgumentException("Price must be > 0.");
            var item = new CartItem { UserId = dto.UserId, ProductId = dto.ProductId, ProductName = dto.ProductName, Price = dto.Price, Quantity = dto.Quantity };
            var result = await _repo.AddOrUpdateAsync(item);
            return Map(result);
        }

        public async Task<bool> RemoveFromCartAsync(int cartItemId) => await _repo.RemoveAsync(cartItemId);

        public async Task ClearCartAsync(int userId) => await _repo.ClearCartAsync(userId);

        private static CartItemDTO Map(CartItem c) => new CartItemDTO { CartItemId = c.CartItemId, UserId = c.UserId, ProductId = c.ProductId, ProductName = c.ProductName, Price = c.Price, Quantity = c.Quantity };
    }
}
