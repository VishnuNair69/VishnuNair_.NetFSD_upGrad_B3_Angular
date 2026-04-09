using Api.Models;
namespace Api.Repositorys
{
    public class ContactRepository : IContactRepository
    {
       public static List<ContactInfo> contacts=new List<ContactInfo>();
       private static int _id=1;
       public async Task<List<ContactInfo>> GetContactInfos()
        {
            return await Task.FromResult(contacts);
        }
        public async Task<ContactInfo> GetById(int id)
        {
            var contact=contacts.FirstOrDefault(item=>item.ContactId==id);
            return await Task.FromResult(contact);
        }
        public async Task CreateContact(ContactInfo contactInfo)
        {
            contactInfo.ContactId=_id++;
            contacts.Add(contactInfo);
           await Task.CompletedTask;
        }
        public async Task UpdateContactInfo(int id,ContactInfo contactInfo)
        {
            var ex=contacts.Find(item=>item.ContactId==id);
             if (ex == null)
             return;
            ex.FirstName=contactInfo.FirstName;
            ex.LastName=contactInfo.LastName;
            ex.EmailId=contactInfo.EmailId;
            ex.MobileNo=contactInfo.MobileNo;
            ex.Company=contactInfo.Company;
            ex.Department=contactInfo.Department;
            await Task.CompletedTask;

        }
        public async Task DeleteContactInfo(int id)
        {
            var contact=contacts.Find(item=>item.ContactId==id);
            if (contact == null)
            return;
            contacts.Remove(contact);
            await Task.CompletedTask;
        }
    }
}