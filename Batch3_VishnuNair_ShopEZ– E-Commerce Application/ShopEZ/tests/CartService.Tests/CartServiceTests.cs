using CartService.DTOs;
using CartService.Models;
using CartService.Repositories;
using CartService.Services;
using Moq;
using Xunit;

namespace CartService.Tests
{
    public class CartServiceTests
    {
        private readonly Mock<ICartRepository> _mockRepo;
        private readonly CartServiceImpl _service;

        public CartServiceTests()
        {
            _mockRepo = new Mock<ICartRepository>();
            _service = new CartServiceImpl(_mockRepo.Object);
        }

        // ── ADD TO CART TESTS ───────────────────────────────────────────────

        [Fact]
        public async Task AddToCart_ValidItem_ReturnsCartItemDTO()
        {
            var dto = new AddToCartDTO { UserId = 1, ProductId = 5, ProductName = "Headphones", Price = 3500, Quantity = 2 };
            var cartItem = new CartItem { CartItemId = 1, UserId = 1, ProductId = 5, ProductName = "Headphones", Price = 3500, Quantity = 2 };

            _mockRepo.Setup(r => r.AddOrUpdateAsync(It.IsAny<CartItem>())).ReturnsAsync(cartItem);

            var result = await _service.AddToCartAsync(dto);
            Assert.Equal(5, result.ProductId);
            Assert.Equal(2, result.Quantity);
        }

        [Fact]
        public async Task AddToCart_ZeroQuantity_ThrowsArgumentException()
        {
            var dto = new AddToCartDTO { UserId = 1, ProductId = 1, Price = 100, Quantity = 0 };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddToCartAsync(dto));
        }

        [Fact]
        public async Task AddToCart_ZeroPrice_ThrowsArgumentException()
        {
            var dto = new AddToCartDTO { UserId = 1, ProductId = 1, Price = 0, Quantity = 1 };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.AddToCartAsync(dto));
        }

        // ── REMOVE FROM CART TESTS ──────────────────────────────────────────

        [Fact]
        public async Task RemoveFromCart_ExistingItem_ReturnsTrue()
        {
            _mockRepo.Setup(r => r.RemoveAsync(1)).ReturnsAsync(true);
            var result = await _service.RemoveFromCartAsync(1);
            Assert.True(result);
        }

        [Fact]
        public async Task RemoveFromCart_NonExistingItem_ReturnsFalse()
        {
            _mockRepo.Setup(r => r.RemoveAsync(999)).ReturnsAsync(false);
            var result = await _service.RemoveFromCartAsync(999);
            Assert.False(result);
        }

        // ── GET CART TESTS ──────────────────────────────────────────────────

        [Fact]
        public async Task GetCart_ReturnsAllItemsForUser()
        {
            var items = new List<CartItem>
            {
                new CartItem { CartItemId = 1, UserId = 1, ProductId = 1, ProductName = "Laptop", Price = 50000, Quantity = 1 },
                new CartItem { CartItemId = 2, UserId = 1, ProductId = 2, ProductName = "Mouse", Price = 1500, Quantity = 2 }
            };
            _mockRepo.Setup(r => r.GetByUserIdAsync(1)).ReturnsAsync(items);

            var result = (await _service.GetCartAsync(1)).ToList();
            Assert.Equal(2, result.Count);
        }

        // ── CLEAR CART TESTS ────────────────────────────────────────────────

        [Fact]
        public async Task ClearCart_CallsRepositoryClear()
        {
            _mockRepo.Setup(r => r.ClearCartAsync(1)).Returns(Task.CompletedTask);
            await _service.ClearCartAsync(1);
            // Verify ClearCartAsync was called exactly once with userId=1
            _mockRepo.Verify(r => r.ClearCartAsync(1), Times.Once);
        }
    }
}
