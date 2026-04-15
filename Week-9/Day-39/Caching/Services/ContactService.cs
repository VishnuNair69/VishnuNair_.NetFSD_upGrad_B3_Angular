using Microsoft.Extensions.Caching.Memory;
using WebApplication16.Models;
using WebApplication16.Repository;

namespace WebApplication16.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repo;
        private readonly IMemoryCache _cache;

        private const string CONTACT_LIST_KEY = "contact_list";

        public ContactService(IContactRepository repo, IMemoryCache cache)
        {
            _repo = repo;
            _cache = cache;
        }

        // GET ALL
        public object GetAllContacts()
        {
            if (_cache.TryGetValue(CONTACT_LIST_KEY, out List<Contact> cachedData))
            {
                return new
                {
                    message = "Returned from CACHE",
                    data = cachedData
                };
            }

            var data = _repo.GetAll();

            _cache.Set(CONTACT_LIST_KEY, data, TimeSpan.FromSeconds(60));

            return new
            {
                message = "Returned from DATABASE",
                data = data
            };
        }

        // GET BY ID
        public object GetContactById(int id)
        {
            string key = $"contact_{id}";

            if (_cache.TryGetValue(key, out Contact cachedContact))
            {
                return new
                {
                    message = "Returned from CACHE",
                    data = cachedContact
                };
            }

            var contact = _repo.GetById(id);

            if (contact != null)
            {
                _cache.Set(key, contact, TimeSpan.FromSeconds(60));
            }

            return new
            {
                message = "Returned from DATABASE",
                data = contact
            };
        }
    }
}