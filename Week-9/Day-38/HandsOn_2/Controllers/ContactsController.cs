using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication14.Data;
using WebApplication14.Exceptions;
using WebApplication14.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication14.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly ILogger<ContactsController> _logger;

        public ContactsController(AppDbContext context, ILogger<ContactsController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET ALL
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _context.Contacts.ToListAsync());
        }

        // GET BY ID
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                throw new NotFoundException("Contact not found");

            return Ok(contact);
        }

        // CREATE
        [HttpPost]
        public async Task<IActionResult> Create(Contact contact)
        {
            if (string.IsNullOrWhiteSpace(contact.Name))
                throw new BadRequestException("Name is required");

            _context.Contacts.Add(contact);
            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact created: {Name}", contact.Name);

            return Ok(contact);
        }

        // UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, Contact updated)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                throw new NotFoundException("Contact not found");

            contact.Name = updated.Name;
            contact.Email = updated.Email;
            contact.Phone = updated.Phone;

            await _context.SaveChangesAsync();

            _logger.LogInformation("Contact updated: {Id}", id);

            return Ok(contact);
        }

        // DELETE
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _context.Contacts.FindAsync(id);

            if (contact == null)
                throw new NotFoundException("Contact not found");

            _context.Contacts.Remove(contact);
            await _context.SaveChangesAsync();

            _logger.LogWarning("Contact deleted: {Id}", id);

            return Ok("Deleted successfully");
        }
    }
}
