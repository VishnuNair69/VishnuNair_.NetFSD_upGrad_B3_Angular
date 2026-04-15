using WebApplication16.Models;

namespace WebApplication16.Services
{
    public interface IContactService
    {
        object GetAllContacts();
        object GetContactById(int id);
    }
}
