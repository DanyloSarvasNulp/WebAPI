﻿using System.Threading.Tasks;
using WebAPI.Entities.Models;

namespace WebAPI.Repository.Interfaces
{
    public interface IContactRepository : IGenericRepository<Contact>
    {
        Task<Contact> FindDuplicateEmail(string name);
    }
}