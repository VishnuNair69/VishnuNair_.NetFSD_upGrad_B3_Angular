using OrderService.DTOs;
namespace OrderService.Services
{
    public interface IOrderService
    {
        Task<IEnumerable<OrderDTO>> GetAllAsync();
        Task<OrderDTO?> GetByIdAsync(int id);
        Task<OrderDTO> CreateAsync(CreateOrderDTO dto);
    }
}
