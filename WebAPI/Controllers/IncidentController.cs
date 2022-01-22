using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
// using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        private readonly IIncidentRepository _incident;
        private readonly IAccountRepository _account;
        private readonly IContactRepository _contact;

        public IncidentController (
            IIncidentRepository incident,
            IAccountRepository account,
            IContactRepository contact)
        {
            _incident = incident;
            _account = account;
            _contact = contact;
        }

        [HttpPost]
        public async Task<IActionResult> Post (string accountName, 
                string firstName, 
                string lastName, 
                string contactEmail, 
                string incidentDescription)
        {
            
            var incident = new Incident();

            var account = await _account.FindByName(accountName);
            if (account == null) return NotFound();

            var contact = await _contact.FindByEmail(contactEmail);
            
            if (contact != null)
            {
                contact.FirstName = firstName;
                contact.LastName = lastName;
                
                incident.AccountId = account.Id;
                incident.Description = incidentDescription;
            }

            else
            {
                var newContact = new Contact
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = contactEmail,
                    Accounts = new List<Account> {account}
                };

                account.Contact = newContact;
                account.ContactId = newContact.Id;

                incident.Account = account;
                incident.AccountId = account.Id;
                incident.Description = incidentDescription;
            }

            _incident.Create(incident);
            _incident.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<Incident> Get(string name)
        {
            var incident = await _incident.GetByName(name);
            return incident;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Incident>> GetAll()
        {
            return await _incident.GetAll();
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var incident = await _incident.GetByName(name);
            if (incident == null) return NotFound();
            
            _incident.Delete(incident);
            _incident.Save();
            return Ok();
        }
    }
}