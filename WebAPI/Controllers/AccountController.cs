using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Entities;
using WebAPI.Entities.Models;

namespace WebAPI.Controllers
{
    
    public class AccountController
    {
        private readonly WebApiDbContext _context;
        private readonly DbSet<Account> _dbSet;

        public AccountController(WebApiDbContext context)
        {
            _context = context;
            _dbSet = context.Set<Account>();
        }
    }
}