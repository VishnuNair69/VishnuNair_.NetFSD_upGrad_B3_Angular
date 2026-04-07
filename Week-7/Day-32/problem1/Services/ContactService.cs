 using EXAPM.Models;
 using System.Collections.Generic;
 namespace EXAPM.Services
{
    public class ContactService :IContactService
    {
         private static List<ContactInfo> contacts = new List<ContactInfo>();
        public List<ContactInfo> GetAllContacts()
        {
            return contacts;
        }
        public  ContactInfo GetContactById(int id)
        {
            return contacts.FirstOrDefault(contact=>contact.ContactId==id);
        }
        public  void AddContact(ContactInfo contact)
        {
            contacts.Add(contact);
        }
    }
}