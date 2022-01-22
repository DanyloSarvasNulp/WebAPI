using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository
{
    public class IncidentRepository : IIncidentRepository
    {
        private readonly WebApiDbContext _context;
        
        private readonly DbSet<Incident> _dbSet;

        public IncidentRepository (WebApiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Incident>();
        }

        public Task<List<Incident>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public Task<Incident> GetByName(string name)
        {
            return _dbSet.SingleOrDefaultAsync(x => x.Name == name);
        }
        
        public void Create(Incident entity)
        {
            _dbSet.Add(entity);
        }
        
        public void Update(Incident entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(Incident entity)
        {
            _dbSet.Remove(entity);
        }
    }
}