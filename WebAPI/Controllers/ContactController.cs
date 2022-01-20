using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contact;

        public ContactController (IContactRepository contact)
        {
            _contact = contact;
        }

        [HttpPost]
        public IActionResult Post(string firstName, string lastName, string email)
        {
            var duplicateEmail = _contact.FindDuplicateEmail(email);
            if (duplicateEmail != null) return Conflict();
            
            var contact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            _contact.Create(contact);
            _contact.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<Contact> Get(int id)
        {
            var contact = await _contact.GetById(id);
            return contact;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Contact>> GetAll()
        {
            var contacts = await _contact.GetAll();
            return contacts;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _contact.GetById(id);
            if (contact == null) return NotFound();
            
            _contact.Delete(contact);
            _contact.Save();
            return Ok();
        }
    }
}