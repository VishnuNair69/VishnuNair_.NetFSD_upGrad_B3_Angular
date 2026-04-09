using Api.Models;
using Api.Repositorys;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _repo;

        public ContactController(IContactRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var data = await _repo.GetContactInfos();
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById(int id)
        {
            var data = await _repo.GetById(id);

            if (data == null)
                return NotFound("Contact not found");

            return Ok(data);
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(ContactInfo contactInfo)
        {
            await _repo.CreateContact(contactInfo);

            return Created("contact created sucessfully",contactInfo);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact(int id, ContactInfo contactInfo)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                return NotFound("Contact not found");

            await _repo.UpdateContactInfo(id, contactInfo);

            return Ok("Updated successfully");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContact(int id)
        {
            var existing = await _repo.GetById(id);

            if (existing == null)
                return NotFound("Contact not found");

            await _repo.DeleteContactInfo(id);

            return Ok("Deleted successfully");
        }
    }
}