using System.Threading.Tasks;
using WebAPI.Entities;
using WebAPI.Entities.Models;

namespace WebAPI.Repository.Interfaces
{
    public interface IAccountRepository : IGenericRepository<Account>
    {
        Task<Account> FindByName(string name);
    }
}