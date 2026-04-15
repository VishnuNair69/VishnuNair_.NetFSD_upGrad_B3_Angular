using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using WebApplication18.Models;
using WebApplication18.Models.Data;
using Microsoft.EntityFrameworkCore;

namespace WebApplication18.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;

        public ContactsController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ GET with Rate Limiting
        [HttpGet]
        [EnableRateLimiting("fixed")]
        public async Task<IActionResult> GetContacts()
        {
            var data = await _context.Contacts.ToListAsync();
            return Ok(data);
        }

        // ✅ POST to insert data
        [HttpPost]
        public async Task<IActionResult> AddContact(Contact contact)
        {
            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();
            return Ok(contact);
        }
    }
}
