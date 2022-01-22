using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities.Models;
using WebAPI.Repository;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ContactController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public ContactController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post(string firstName, string lastName, string email)
        {
            var duplicateContact = await _unitOfWork.Contacts.FindByEmail(email);
            if (duplicateContact != null) return Conflict();
            
            var contact = new Contact
            {
                FirstName = firstName,
                LastName = lastName,
                Email = email
            };

            _unitOfWork.Contacts.Create(contact);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<Contact> Get(int id)
        {
            var contact = await _unitOfWork.Contacts.GetById(id);
            return contact;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Contact>> GetAll()
        {
            var contacts = await _unitOfWork.Contacts.GetAll();
            return contacts;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var contact = await _unitOfWork.Contacts.GetById(id);
            if (contact == null) return NotFound();
            
            _unitOfWork.Contacts.Delete(contact);
            _unitOfWork.Save();
            return Ok();
        }
    }
}