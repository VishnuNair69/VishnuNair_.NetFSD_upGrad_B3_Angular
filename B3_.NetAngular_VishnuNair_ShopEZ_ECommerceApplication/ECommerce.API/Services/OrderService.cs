using ECommerce.API.Data;
using ECommerce.API.DTOs;
using ECommerce.API.Models;
using Microsoft.EntityFrameworkCore;

namespace ECommerce.API.Services
{
    // Contains core order processing business logic
    public class OrderService : IOrderService
    {
        private readonly ApplicationDbContext _context;

        public OrderService(ApplicationDbContext context)
        {
            _context = context;
        }

        // ─── Core Order Creation Logic ───────────────────────────────────────────
        public async Task<OrderDTO> CreateOrderAsync(CreateOrderDTO dto)
        {
            // Step 1: Validate cart is not empty
            if (dto.CartItems == null || dto.CartItems.Count == 0)
                throw new ArgumentException("Cart cannot be empty.");

            // Step 2: Validate each item — product existence + quantity
            var orderItems = new List<OrderItem>();

            foreach (var cartItem in dto.CartItems)
            {
                // Validate quantity
                if (cartItem.Quantity <= 0)
                    throw new ArgumentException($"Quantity for ProductId {cartItem.ProductId} must be greater than zero.");

                // Validate product existence
                var product = await _context.Products.FindAsync(cartItem.ProductId);
                if (product == null)
                    throw new KeyNotFoundException($"Product with ID {cartItem.ProductId} not found.");

                // Build OrderItem — using product's current price (snapshot at time of order)
                orderItems.Add(new OrderItem
                {
                    ProductId = cartItem.ProductId,
                    Quantity = cartItem.Quantity,
                    Price = product.Price   // store price at time of purchase
                });
            }

            // Step 3: Calculate TotalAmount using LINQ
            decimal totalAmount = orderItems.Sum(item => item.Price * item.Quantity);

            // Step 4: Create Order entity
            var order = new Order
            {
                UserId = dto.UserId,
                OrderDate = DateTime.UtcNow,
                TotalAmount = totalAmount,
                OrderItems = orderItems
            };

            // Step 5: Save to database
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            // Step 6: Return DTO
            return await MapToOrderDTOAsync(order.OrderId);
        }

        // ─── Get All Orders ──────────────────────────────────────────────────────
        public async Task<IEnumerable<OrderDTO>> GetAllOrdersAsync()
        {
            var orders = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .ToListAsync();

            return orders.Select(o => MapToDTO(o));
        }

        // ─── Get Order by ID ─────────────────────────────────────────────────────
        public async Task<OrderDTO?> GetOrderByIdAsync(int id)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstOrDefaultAsync(o => o.OrderId == id);

            if (order == null) return null;
            return MapToDTO(order);
        }

        // ─── Helper: load fresh order from DB and map to DTO ────────────────────
        private async Task<OrderDTO> MapToOrderDTOAsync(int orderId)
        {
            var order = await _context.Orders
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.Product)
                .FirstAsync(o => o.OrderId == orderId);

            return MapToDTO(order);
        }

        // ─── Helper: map Order entity → OrderDTO ────────────────────────────────
        private static OrderDTO MapToDTO(Order order)
        {
            return new OrderDTO
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                OrderDate = order.OrderDate,
                TotalAmount = order.TotalAmount,
                Items = order.OrderItems.Select(oi => new OrderItemDTO
                {
                    OrderItemId = oi.OrderItemId,
                    ProductId = oi.ProductId,
                    ProductName = oi.Product?.Name ?? "Unknown",
                    Quantity = oi.Quantity,
                    Price = oi.Price
                }).ToList()
            };
        }
    }
}
