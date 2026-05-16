using Moq;
using ProductService.DTOs;
using ProductService.Models;
using ProductService.Repositories;
using ProductService.Services;
using Xunit;

namespace ProductService.Tests
{
    public class ProductServiceTests
    {
        private readonly Mock<IProductRepository> _mockRepo;
        private readonly ProductServiceImpl _service;

        public ProductServiceTests()
        {
            _mockRepo = new Mock<IProductRepository>();
            _service = new ProductServiceImpl(_mockRepo.Object);
        }

        // ── CREATE PRODUCT TESTS ────────────────────────────────────────────

        [Fact]
        public async Task Create_ValidProduct_ReturnsProductDTO()
        {
            var dto = new CreateProductDTO { Name = "Laptop", Description = "Fast laptop", Price = 50000, Stock = 10, ImageUrl = "" };
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<Product>()))
                     .ReturnsAsync(new Product { ProductId = 1, Name = "Laptop", Price = 50000, Stock = 10 });

            var result = await _service.CreateAsync(dto);
            Assert.Equal("Laptop", result.Name);
            Assert.Equal(1, result.ProductId);
        }

        [Fact]
        public async Task Create_EmptyName_ThrowsArgumentException()
        {
            var dto = new CreateProductDTO { Name = "", Price = 100, Stock = 5 };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task Create_ZeroPrice_ThrowsArgumentException()
        {
            var dto = new CreateProductDTO { Name = "Laptop", Price = 0, Stock = 5 };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        [Fact]
        public async Task Create_NegativeStock_ThrowsArgumentException()
        {
            var dto = new CreateProductDTO { Name = "Laptop", Price = 1000, Stock = -1 };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.CreateAsync(dto));
        }

        // ── SEARCH TESTS ────────────────────────────────────────────────────

        [Fact]
        public async Task Search_ValidKeyword_ReturnsFilteredProducts()
        {
            var products = new List<Product>
            {
                new Product { ProductId = 1, Name = "Laptop Pro", Price = 50000, Stock = 5 },
                new Product { ProductId = 2, Name = "Gaming Mouse", Price = 1500, Stock = 20 }
            };
            _mockRepo.Setup(r => r.SearchAsync("Laptop")).ReturnsAsync(products.Take(1));

            var result = (await _service.SearchAsync("Laptop")).ToList();
            Assert.Single(result);
            Assert.Equal("Laptop Pro", result[0].Name);
        }

        [Fact]
        public async Task Search_EmptyKeyword_ReturnsAllProducts()
        {
            var products = new List<Product> { new Product { ProductId = 1, Name = "A", Price = 100, Stock = 1 } };
            _mockRepo.Setup(r => r.GetAllAsync()).ReturnsAsync(products);

            var result = await _service.SearchAsync("");
            Assert.Single(result);
        }

        // ── DELETE TESTS ────────────────────────────────────────────────────

        [Fact]
        public async Task Delete_ExistingProduct_ReturnsTrue()
        {
            _mockRepo.Setup(r => r.DeleteAsync(1)).ReturnsAsync(true);
            var result = await _service.DeleteAsync(1);
            Assert.True(result);
        }

        [Fact]
        public async Task Delete_NonExistingProduct_ReturnsFalse()
        {
            _mockRepo.Setup(r => r.DeleteAsync(999)).ReturnsAsync(false);
            var result = await _service.DeleteAsync(999);
            Assert.False(result);
        }
    }
}
