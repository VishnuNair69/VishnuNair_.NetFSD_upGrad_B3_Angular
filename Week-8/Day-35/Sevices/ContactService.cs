using webapp25.Models;
using webapp25.Repository;

namespace webapp25.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;

        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }

        public List<ContactInfo> GetAllContacts()
        {
            return _repo.GetAllContacts();
        }

        public ContactInfo GetContactById(int id)
        {
            return _repo.GetContactById(id);
        }

        public void AddContact(ContactInfo contact)
        {
            _repo.AddContact(contact);
        }

        public void UpdateContact(ContactInfo contact)
        {
            _repo.UpdateContact(contact);
        }

        public void DeleteContact(int id)
        {
            _repo.DeleteContact(id);
        }

        public List<Company> GetCompanies()
        {
            return _repo.GetCompanies();
        }

        public List<Department> GetDepartments()
        {
            return _repo.GetDepartments();
        }
    }
}