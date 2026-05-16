using Moq;
using OrderService.DTOs;
using OrderService.Models;
using OrderService.Repositories;
using OrderService.Services;
using Xunit;

namespace OrderService.Tests
{
    public class OrderServiceTests
    {
        private readonly Mock<IOrderRepository> _mockRepo;
        private readonly OrderServiceImpl _service;

        public OrderServiceTests()
        {
            _mockRepo = new Mock<IOrderRepository>();
            _service = new OrderServiceImpl(_mockRepo.Object);
        }

        // ── CREATE ORDER TESTS ──────────────────────────────────────────────

        [Fact]
        public async Task CreateOrder_ValidCart_ReturnsOrderDTO()
        {
            var dto = new CreateOrderDTO
            {
                UserId = 1,
                CartItems = new List<CartItemDTO>
                {
                    new CartItemDTO { ProductId = 1, ProductName = "Laptop", Quantity = 2, Price = 50000 },
                    new CartItemDTO { ProductId = 2, ProductName = "Mouse", Quantity = 1, Price = 1500 }
                }
            };

            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Order>()))
                     .ReturnsAsync((Order o) => { o.OrderId = 1; return o; });

            var result = await _service.CreateAsync(dto);

            Assert.Equal(1, result.UserId);
            Assert.Equal(101500m, result.TotalAmount); // 2*50000 + 1*1500
            Assert.Equal(2, result.Items.Count);
        }

        [Fact]
        public async Task CreateOrder_EmptyCart_ThrowsArgumentException()
        {
            var dto = new CreateOrderDTO { UserId = 1, CartItems = new List<CartItemDTO>() };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateOrder_NullCart_ThrowsArgumentException()
        {
            var dto = new CreateOrderDTO { UserId = 1, CartItems = null! };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateOrder_ZeroQuantity_ThrowsArgumentException()
        {
            var dto = new CreateOrderDTO
            {
                UserId = 1,
                CartItems = new List<CartItemDTO>
                {
                    new CartItemDTO { ProductId = 1, ProductName = "Laptop", Quantity = 0, Price = 50000 }
                }
            };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task CreateOrder_CorrectTotalCalculation_UsingLINQ()
        {
            // Tests that LINQ Sum correctly calculates total
            var dto = new CreateOrderDTO
            {
                UserId = 1,
                CartItems = new List<CartItemDTO>
                {
                    new CartItemDTO { ProductId = 1, ProductName = "A", Quantity = 3, Price = 1000 },
                    new CartItemDTO { ProductId = 2, ProductName = "B", Quantity = 2, Price = 500 }
                }
            };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Order>()))
                     .ReturnsAsync((Order o) => { o.OrderId = 1; return o; });

            var result = await _service.CreateAsync(dto);
            Assert.Equal(4000m, result.TotalAmount); // 3*1000 + 2*500 = 4000
        }

        // ── GET ORDER TESTS ─────────────────────────────────────────────────

        [Fact]
        public async Task GetById_ExistingOrder_ReturnsOrderDTO()
        {
            var order = new Order { OrderId = 1, UserId = 1, TotalAmount = 5000, OrderItems = new List<OrderItem>() };
            _mockRepo.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(order);

            var result = await _service.GetByIdAsync(1);
            Assert.NotNull(result);
            Assert.Equal(1, result!.OrderId);
        }

        [Fact]
        public async Task GetById_NonExistingOrder_ReturnsNull()
        {
            _mockRepo.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Order?)null);
            var result = await _service.GetByIdAsync(999);
            Assert.Null(result);
        }
    }
}
