using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication17.Repository;

namespace WebApplication17.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactsController : ControllerBase
    {
        private readonly IContactRepository _repository;

        public ContactsController(IContactRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IActionResult> GetContacts(int pageNumber = 1, int pageSize = 5)
        {
            var result = await _repository.GetContactsAsync(pageNumber, pageSize);
            return Ok(result);
        }
    }
}
