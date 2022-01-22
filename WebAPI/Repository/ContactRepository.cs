using System.Linq;
using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository
{
    public class ContactRepository : GenericRepository<Contact>, IContactRepository
    {
        public ContactRepository(WebApiDbContext context) : base(context)
        {
            
        }
        
        public async Task<bool> IsThereEmailDuplicate(string email)
        {
            var contacts = await GetAll();
            var duplicateContacts = contacts.SingleOrDefault(c => c.Email == email);
            return duplicateContacts != null;
        }
    }
}