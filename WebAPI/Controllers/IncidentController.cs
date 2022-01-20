using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
// using System.Web.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Entities.Models;

namespace WebAPI.Controllers
{
    [ApiController]
    [Microsoft.AspNetCore.Mvc.Route("[controller]")]
    public class IncidentController : ControllerBase
    {
        // я не можу використовувати патерн репозиторії, бо incident не має
        // поля int Id. Кастилі, аля умовний primary key Id, робити не хочеться
        private readonly WebApiDbContext _context;
        private readonly DbSet<Account> _accountDbSet;
        private readonly DbSet<Incident> _incidentDbSet;
        private readonly DbSet<Contact> _contactDbSet;

        public IncidentController (WebApiDbContext context)
        {
            _context = context;
            _incidentDbSet = context.Set<Incident>();
            _accountDbSet = context.Set<Account>();
            _contactDbSet = context.Set<Contact>();
        }

        [HttpPost]
        public async Task<IActionResult> Post (string accountName, 
                string firstName, 
                string lastName, 
                string contactEmail, 
                string incidentDescription)
        {
            
            var incident = new Incident();
            
            var account = await _accountDbSet
                .SingleOrDefaultAsync(a => a.Name == accountName);
            
            if (account == null) return NotFound();

            var preCreatedContact = await _contactDbSet
                .SingleOrDefaultAsync(c => c.Email == contactEmail);
            account.Contact = preCreatedContact;
            if (preCreatedContact != null)
            {
                account.Contact.FirstName = firstName;
                account.Contact.LastName = lastName;

                incident.Account = account;
                incident.AccountId = account.Id;
                incident.Description = incidentDescription;
            }

            else
            {
                var contact = new Contact()
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Email = contactEmail,
                    Accounts = new List<Account>(){account}
                };

                account.Contact = contact;
                account.ContactId = contact.Id;

                incident.Account = account;
                incident.AccountId = account.Id;
                incident.Description = incidentDescription;
            }

            _incidentDbSet.Add(incident);
            await _context.SaveChangesAsync();
            return Ok();
        }

        [Microsoft.AspNetCore.Mvc.HttpGet]
        public Incident Get(string name)
        {
            var incident = _incidentDbSet
                .SingleOrDefault(i => i.Name == name);
            return incident;
        }
        
        [Route("getAll")]
        [HttpGet]
        public async Task<List<Incident>> GetAll()
        {
            return await _incidentDbSet.ToListAsync();
        }
        
        
        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var incident = await _incidentDbSet.FindAsync(id);
            _incidentDbSet.Remove(incident);
            await _context.SaveChangesAsync();
            
            return Ok();
        }
    }
}