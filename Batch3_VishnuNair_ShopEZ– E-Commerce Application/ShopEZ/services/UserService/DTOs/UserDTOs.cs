namespace UserService.DTOs
{
    public class UserDTO { public int UserId { get; set; } public string Name { get; set; } = ""; public string Email { get; set; } = ""; public string Role { get; set; } = ""; }
    public class RegisterDTO { public string Name { get; set; } = ""; public string Email { get; set; } = ""; public string Password { get; set; } = ""; public string Role { get; set; } = "Customer"; }
    public class LoginDTO { public string Email { get; set; } = ""; public string Password { get; set; } = ""; }
}
