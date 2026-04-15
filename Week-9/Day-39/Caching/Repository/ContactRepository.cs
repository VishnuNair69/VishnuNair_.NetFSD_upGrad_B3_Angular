using WebApplication16.Models;
namespace WebApplication16.Repository
{
    public class ContactRepository : IContactRepository
    {
        private readonly List<Contact> _contacts = new()
        {
            new Contact { Id = 1, Name = "Geetha", Email="geetha@gmail.com"},
            new Contact { Id = 2, Name = "Chandhu", Email="chandu@gmail.com"},
            new Contact { Id = 3, Name = "pooji", Email="pooji@gmail.com"}
        };
        public List<Contact> GetAll() => _contacts;
        public Contact GetById(int id)
        {
            return _contacts.FirstOrDefault(x => x.Id == id);
        }
    }
}
