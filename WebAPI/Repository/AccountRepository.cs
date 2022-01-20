using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository
{
    public class AccountRepository : GenericRepository<Account>, IAccountRepository
    {
        public AccountRepository(WebApiDbContext context) : base(context) 
        {

        }
        
        public async Task<Account> FindDuplicateName(string name)
        {
            var accounts = await GetAll();
            var duplicateAccounts = accounts.SingleOrDefault(a => a.Name == name);
            if (duplicateAccounts == null) return null;
            return duplicateAccounts;
        }
    }
}