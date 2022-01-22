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
        private readonly IUnitOfWork _unitOfWork;


        public AccountController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

        }
        
        [HttpPost]
        public async Task<IActionResult> Post(int contactId, string name)
        {
            var duplicateAccount = await _unitOfWork.Accounts.FindByName(name);
            if (duplicateAccount != null) return Conflict();

            var contact = await _unitOfWork.Contacts.GetById(contactId);
            if (contact == null) return NotFound();
            
            var account = new Account
            {
                Name = name,
                ContactId = contactId,
            };
            
            _unitOfWork.Accounts.Create(account);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<Account> Get(int id)
        {
            var account = await _unitOfWork.Accounts.GetById(id);
            return account;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Account>> GetAll()
        {
            var accounts = await _unitOfWork.Accounts.GetAll();
            return accounts;
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var account = await _unitOfWork.Accounts.GetById(id);
            if (account == null) return NotFound();
            
            _unitOfWork.Accounts.Delete(account);
            _unitOfWork.Save();
            return Ok();
        }
    }
}