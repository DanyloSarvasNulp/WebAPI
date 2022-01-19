using System.Collections.Generic;
using System.Threading.Tasks;
using WebAPI.Entities.Models;

namespace WebAPI.Repository
{
    public interface IGenericRepository<T>
        where T : class, IEntity
    {
        Task<List<T>> GetAll();
        Task<T> GetById(int id);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Save();
    }
}