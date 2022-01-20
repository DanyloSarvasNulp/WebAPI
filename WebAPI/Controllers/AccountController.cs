using System.Collections.Generic;
using System.Data;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AccountController : ControllerBase
    {
        private readonly IAccountRepository _account;
        private readonly IContactRepository _contact;

        public AccountController(
            IAccountRepository account, 
            IContactRepository contact)
        {
            _account = account;
            _contact = contact;
        }
        
        [HttpPost]
        public async Task<IActionResult> Post(int contactId, string name)
        {
            var duplicateAccount = await _account.FindDuplicateName(name);
            if (duplicateAccount != null) return Conflict();

            var contact = await _contact.GetById(contactId);
            if (contact == null) return NotFound();
            
            var account = new Account
            {
                Name = name,
                ContactId = contactId,
                Contact = contact
            };
            
            _account.Create(account);
            
            _account.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<Account> Get(int id)
        {
            var account = await _account.GetById(id);
            return account;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Account>> GetAll()
        {
            var accounts = await _account.GetAll();
            return accounts;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _account.GetById(id);
            _account.Delete(account);
            _account.Save();
            return Ok();
        }
    }
}