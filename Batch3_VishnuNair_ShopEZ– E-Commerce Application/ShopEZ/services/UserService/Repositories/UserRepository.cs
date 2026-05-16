using Microsoft.EntityFrameworkCore;
using UserService.Data;
using UserService.Models;
namespace UserService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _context;
        public UserRepository(UserDbContext context) { _context = context; }
        public async Task<IEnumerable<User>> GetAllAsync() => await _context.Users.ToListAsync();
        public async Task<User?> GetByEmailAsync(string email) => await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        public async Task<User?> GetByIdAsync(int id) => await _context.Users.FindAsync(id);
        public async Task<User> AddAsync(User user) { _context.Users.Add(user); await _context.SaveChangesAsync(); return user; }
        public async Task<bool> EmailExistsAsync(string email) => await _context.Users.AnyAsync(u => u.Email == email);
    }
}
