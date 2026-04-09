using Api.Models;
namespace Api.Repositorys
{
    public interface IContactRepository
    {
        Task<List<ContactInfo>> GetContactInfos();
        Task<ContactInfo> GetById(int id);
        Task UpdateContactInfo(int id,ContactInfo contactInfo);
        Task DeleteContactInfo(int id);
       Task CreateContact(ContactInfo contactInfo);
    }
}