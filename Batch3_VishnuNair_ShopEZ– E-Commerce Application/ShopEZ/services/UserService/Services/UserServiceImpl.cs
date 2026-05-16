using UserService.DTOs;
using UserService.Models;
using UserService.Repositories;
namespace UserService.Services
{
    public class UserServiceImpl : IUserService
    {
        private readonly IUserRepository _repo;
        public UserServiceImpl(IUserRepository repo) { _repo = repo; }

        public async Task<IEnumerable<UserDTO>> GetAllUsersAsync()
        {
            var users = await _repo.GetAllAsync();
            return users.Select(u => MapToDTO(u));
        }

        public async Task<UserDTO?> LoginAsync(LoginDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Email) || string.IsNullOrWhiteSpace(dto.Password))
                throw new ArgumentException("Email and password are required.");
            var user = await _repo.GetByEmailAsync(dto.Email);
            if (user == null || user.Password != dto.Password) return null;
            return MapToDTO(user);
        }

        public async Task<UserDTO> RegisterAsync(RegisterDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Name)) throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(dto.Email)) throw new ArgumentException("Email is required.");
            if (string.IsNullOrWhiteSpace(dto.Password)) throw new ArgumentException("Password is required.");
            if (await _repo.EmailExistsAsync(dto.Email)) throw new InvalidOperationException("Email already registered.");
            if (dto.Role != "Admin" && dto.Role != "Customer") dto.Role = "Customer";
            var user = new User { Name = dto.Name, Email = dto.Email, Password = dto.Password, Role = dto.Role };
            var created = await _repo.AddAsync(user);
            return MapToDTO(created);
        }

        private static UserDTO MapToDTO(User u) =>
            new UserDTO { UserId = u.UserId, Name = u.Name, Email = u.Email, Role = u.Role };
    }
}
