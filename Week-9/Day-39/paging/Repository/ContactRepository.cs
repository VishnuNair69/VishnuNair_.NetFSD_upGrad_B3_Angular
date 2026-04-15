using WebApplication17.Data;
using WebApplication17.DTOs;
using WebApplication17.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApplication17.Repository
{
    public class ContactRepository : IContactRepository
    {

        private readonly AppDbContext _context;

        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResponse<Contact>> GetContactsAsync(int pageNumber, int pageSize)
        {
            var totalRecords = await _context.Contacts.CountAsync();

            var contacts = await _context.Contacts
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);

            return new PagedResponse<Contact>
            {
                TotalRecords = totalRecords,
                TotalPages = totalPages,
                CurrentPage = pageNumber,
                PageSize = pageSize,
                Data = contacts
            };
        }
    }
}