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
        private readonly IUnitOfWork _unitOfWork;

        public IncidentController (IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public async Task<IActionResult> Post (string accountName, 
                string firstName, 
                string lastName, 
                string contactEmail, 
                string incidentDescription)
        {
            
            var incident = new Incident();

            var account = await _unitOfWork.Accounts.FindByName(accountName);
            if (account == null) return NotFound();

            var contact = await _unitOfWork.Contacts.FindByEmail(contactEmail);
            
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

            _unitOfWork.Incidents.Create(incident);
            _unitOfWork.Save();
            return Ok();
        }

        [HttpGet]
        public async Task<Incident> Get(string name)
        {
            var incident = await _unitOfWork.Incidents.GetByName(name);
            return incident;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Incident>> GetAll()
        {
            return await _unitOfWork.Incidents.GetAll();
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> Delete(string name)
        {
            var incident = await _unitOfWork.Incidents.GetByName(name);
            if (incident == null) return NotFound();
            
            _unitOfWork.Incidents.Delete(incident);
            _unitOfWork.Save();
            return Ok();
        }
    }
}