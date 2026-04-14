using ECommerce.API.DTOs;

namespace ECommerce.API.Services
{
    // Interface — defines contract for order business logic
    public interface IOrderService
    {
        Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto);
        Task<IEnumerable<OrderDTO>> GetAllOrdersAsync();
        Task<OrderDTO?> GetOrderByIdAsync(int id);
    }
}
