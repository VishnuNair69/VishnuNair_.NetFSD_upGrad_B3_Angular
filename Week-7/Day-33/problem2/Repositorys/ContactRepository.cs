using ContactManagementSystem.Models;
using Microsoft.EntityFrameworkCore;
namespace ContactManagementSystem.Repository
{
    public class ContactRepository:IContactRepository
    {
        public readonly ApplicationDbContext _context;
        public ContactRepository(ApplicationDbContext context)
        {
            _context=context;
        }
        public List<ContactInfo> GetAllContacts()
        {
            return _context.ContactInfos.Include(c=>c.Company).Include(d=>d.Department).ToList();
        }
       public ContactInfo GetContactById(int id)
        {
            return _context.ContactInfos
            .Include(c => c.Company)
            .Include(c => c.Department)
            .FirstOrDefault(c => c.id== id);
        }
        public void AddContact(ContactInfo contact)
        {
              _context.ContactInfos.Add(contact);
              _context.SaveChanges();
        }
        public void UpdateContact(ContactInfo contact)
        {
            _context.ContactInfos.Update(contact);
            _context.SaveChanges();
        }
        public void DeleteContact(int id)
        {
            var c=_context.ContactInfos.Find(id);
            _context.ContactInfos.Remove(c);
            _context.SaveChanges();
        }
        public List<Company> GetCompanies()
{
    return _context.Companies.ToList();
}

public List<Department> GetDepartments()
{
    return _context.Departments.ToList();
}
    }
}