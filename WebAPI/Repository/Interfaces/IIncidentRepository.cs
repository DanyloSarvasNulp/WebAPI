using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Entities.Models;

namespace WebAPI.Repository.Interfaces
{
    public interface IIncidentRepository
    {
        Task<List<Incident>> GetAll();
        Task<Incident> GetByName(string name);
        void Create(Incident entity);
        void Update(Incident entity);
        void Delete(Incident entity);
        void Save();
    }
}