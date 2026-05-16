// xUnit + Moq tests for UserService
// Moq creates fake (mock) implementations of interfaces so we test business logic in isolation
using Moq;
using UserService.DTOs;
using UserService.Models;
using UserService.Repositories;
using UserService.Services;
using Xunit;

namespace UserService.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<IUserRepository> _mockRepo;
        private readonly UserServiceImpl _service;

        public UserServiceTests()
        {
            // Mock creates a fake IUserRepository — no real DB needed
            _mockRepo = new Mock<IUserRepository>();
            _service = new UserServiceImpl(_mockRepo.Object);
        }

        // ── REGISTER TESTS ──────────────────────────────────────────────────

        [Fact]
        public async Task Register_ValidData_ReturnsUserDTO()
        {
            // Arrange
            var dto = new RegisterDTO { Name = "Vishnu", Email = "vishnu@test.com", Password = "pass123", Role = "Customer" };
            _mockRepo.Setup(r => r.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
                     .ReturnsAsync(new User { UserId = 1, Name = dto.Name, Email = dto.Email, Role = dto.Role });

            // Act
            var result = await _service.RegisterAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Vishnu", result.Name);
            Assert.Equal("Customer", result.Role);
        }

        [Fact]
        public async Task Register_EmptyName_ThrowsArgumentException()
        {
            var dto = new RegisterDTO { Name = "", Email = "test@test.com", Password = "pass" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.RegisterAsync(dto));
        }

        [Fact]
        public async Task Register_EmptyEmail_ThrowsArgumentException()
        {
            var dto = new RegisterDTO { Name = "Vishnu", Email = "", Password = "pass" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.RegisterAsync(dto));
        }

        [Fact]
        public async Task Register_DuplicateEmail_ThrowsInvalidOperationException()
        {
            var dto = new RegisterDTO { Name = "Vishnu", Email = "vishnu@test.com", Password = "pass" };
            _mockRepo.Setup(r => r.EmailExistsAsync(dto.Email)).ReturnsAsync(true); // simulate email exists

            await Assert.ThrowsAsync<InvalidOperationException>(() => _service.RegisterAsync(dto));
        }

        [Fact]
        public async Task Register_InvalidRole_DefaultsToCustomer()
        {
            var dto = new RegisterDTO { Name = "Test", Email = "test@test.com", Password = "pass", Role = "SuperUser" };
            _mockRepo.Setup(r => r.EmailExistsAsync(dto.Email)).ReturnsAsync(false);
            _mockRepo.Setup(r => r.AddAsync(It.IsAny<User>()))
                     .ReturnsAsync(new User { UserId = 1, Name = "Test", Email = "test@test.com", Role = "Customer" });

            var result = await _service.RegisterAsync(dto);
            Assert.Equal("Customer", result.Role);
        }

        // ── LOGIN TESTS ─────────────────────────────────────────────────────

        [Fact]
        public async Task Login_ValidCredentials_ReturnsUserDTO()
        {
            var dto = new LoginDTO { Email = "vishnu@test.com", Password = "pass123" };
            _mockRepo.Setup(r => r.GetByEmailAsync(dto.Email))
                     .ReturnsAsync(new User { UserId = 1, Name = "Vishnu", Email = "vishnu@test.com", Password = "pass123", Role = "Customer" });

            var result = await _service.LoginAsync(dto);

            Assert.NotNull(result);
            Assert.Equal("Vishnu", result!.Name);
        }

        [Fact]
        public async Task Login_WrongPassword_ReturnsNull()
        {
            var dto = new LoginDTO { Email = "vishnu@test.com", Password = "wrongpass" };
            _mockRepo.Setup(r => r.GetByEmailAsync(dto.Email))
                     .ReturnsAsync(new User { Email = "vishnu@test.com", Password = "correctpass" });

            var result = await _service.LoginAsync(dto);
            Assert.Null(result);
        }

        [Fact]
        public async Task Login_EmptyEmail_ThrowsArgumentException()
        {
            var dto = new LoginDTO { Email = "", Password = "pass" };
            await Assert.ThrowsAsync<ArgumentException>(() => _service.LoginAsync(dto));
        }

        [Fact]
        public async Task Login_NonExistentEmail_ReturnsNull()
        {
            var dto = new LoginDTO { Email = "nobody@test.com", Password = "pass" };
            _mockRepo.Setup(r => r.GetByEmailAsync(dto.Email)).ReturnsAsync((User?)null);

            var result = await _service.LoginAsync(dto);
            Assert.Null(result);
        }
    }
}
