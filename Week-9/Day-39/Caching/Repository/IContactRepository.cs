using WebApplication16.Models;

namespace WebApplication16.Repository
{
    public interface IContactRepository
    {
        List<Contact> GetAll();
        Contact GetById(int id);
    }
}
