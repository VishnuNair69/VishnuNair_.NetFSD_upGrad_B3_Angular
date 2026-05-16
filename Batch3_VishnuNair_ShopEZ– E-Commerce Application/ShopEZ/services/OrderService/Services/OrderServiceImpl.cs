using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
namespace OrderService.Services
{
    public class OrderServiceImpl : IOrderService
    {
        private readonly IOrderRepository _repo;
        public OrderServiceImpl(IOrderRepository repo) { _repo = repo; }

        public async Task<IEnumerable<OrderDTO>> GetAllAsync() =>
            (await _repo.GetAllAsync()).Select(Map);

        public async Task<OrderDTO?> GetByIdAsync(int id)
        {
            var o = await _repo.GetByIdAsync(id);
            return o == null ? null : Map(o);
        }

        public async Task<OrderDTO> CreateAsync(CreateOrderDTO dto)
        {
            if (dto.CartItems == null || dto.CartItems.Count == 0)
                throw new ArgumentException("Cart cannot be empty.");

            foreach (var item in dto.CartItems)
                if (item.Quantity <= 0)
                    throw new ArgumentException($"Quantity for product {item.ProductId} must be > 0.");

            // Total amount calculated using LINQ
            decimal total = dto.CartItems.Sum(item => item.Price * item.Quantity);

            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = total,
                OrderItems = dto.CartItems.Select(c => new OrderItem
                {
                    ProductId = c.ProductId,
                    ProductName = c.ProductName,
                    Quantity = c.Quantity,
                    Price = c.Price
                }).ToList()
            };

            var created = await _repo.AddAsync(order);
            return Map(created);
        }

        private static OrderDTO Map(Order o) => new OrderDTO
        {
            OrderId = o.OrderId, UserId = o.UserId, OrderDate = o.OrderDate, TotalAmount = o.TotalAmount,
            Items = o.OrderItems.Select(i => new OrderItemDTO
            { OrderItemId = i.OrderItemId, ProductId = i.ProductId, ProductName = i.ProductName, Quantity = i.Quantity, Price = i.Price }).ToList()
        };
    }
}
