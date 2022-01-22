using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Entities.Models;

namespace WebAPI.Repository
{
    public class GenericRepository<T> : IGenericRepository<T>
        where T : class, IEntity
    {
        private readonly WebApiDbContext _context;
        
        private readonly DbSet<T> _dbSet;

        public GenericRepository (WebApiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public Task<List<T>> GetAll()
        {
            return _dbSet.ToListAsync();
        }

        public Task<T> GetById(int id)
        {
            return _dbSet.SingleOrDefaultAsync(x => x.Id == id);
        }
        
        public void Create(T entity)
        {
            _dbSet.Add(entity);
        }
        
        public void Update(T entity)
        {
            _dbSet.Update(entity);
        }

        public void Delete(T entity)
        {
            _dbSet.Remove(entity);
        }
    }
}