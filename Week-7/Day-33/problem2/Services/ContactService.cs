using ContactManagementSystem.Models;
using ContactManagementSystem.Repository;
namespace ContactManagementSystem.Services
{
    public class ContactService:IContactService
    {
        private readonly IContactRepository _repository;
        public ContactService(IContactRepository repository)
        {
            _repository=repository;
        }
        public List<ContactInfo> GetAllContacts()
        {
            return _repository.GetAllContacts();
        }
       public ContactInfo GetContactById(int id)
        {
            return _repository.GetContactById(id);
        }
        public void AddContact(ContactInfo contact)
        {
              _repository.AddContact(contact);
        }
        public void UpdateContact(ContactInfo contact)
        {
            _repository.UpdateContact(contact);
        }
        public void DeleteContact(int id)
        {
           _repository.DeleteContact(id);
        }
        public List<Company> GetCompanies()
{
    return _repository.GetCompanies();
}

public List<Department> GetDepartments()
{
    return _repository.GetDepartments();
}
    }
}