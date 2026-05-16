using UserService.DTOs;
namespace UserService.Services
{
    public interface IUserService
    {
        Task<IEnumerable<UserDTO>> GetAllUsersAsync();
        Task<UserDTO?> LoginAsync(LoginDTO dto);
        Task<UserDTO> RegisterAsync(RegisterDTO dto);
    }
}
