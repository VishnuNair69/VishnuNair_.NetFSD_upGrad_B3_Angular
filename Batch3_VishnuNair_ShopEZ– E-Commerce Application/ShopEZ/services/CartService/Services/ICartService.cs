using CartService.DTOs;
namespace CartService.Services
{
    public interface ICartService
    {
        Task<IEnumerable<CartItemDTO>> GetCartAsync(int userId);
        Task<CartItemDTO> AddToCartAsync(AddToCartDTO dto);
        Task<bool> RemoveFromCartAsync(int cartItemId);
        Task ClearCartAsync(int userId);
    }
}
