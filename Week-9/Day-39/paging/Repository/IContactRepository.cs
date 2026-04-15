using WebApplication17.DTOs;
using WebApplication17.Models;

namespace WebApplication17.Repository
{
    public interface IContactRepository
    {
        Task<PagedResponse<Contact>> GetContactsAsync(int pageNumber, int pageSize);
    }
}
