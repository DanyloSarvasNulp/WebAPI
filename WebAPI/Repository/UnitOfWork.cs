using System;
using WebAPI.Entities;
using WebAPI.Entities.Models;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly WebApiDbContext _dbContext;
        
        private IContactRepository _contactRepository;
        private IAccountRepository _accountRepository;
        private IIncidentRepository _incidentRepository;

        private bool _disposed;

        public UnitOfWork(WebApiDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IAccountRepository Accounts
        {
            get
            {
                if (_accountRepository == null)
                {
                    _accountRepository = new AccountRepository(_dbContext);
                }

                return _accountRepository;
            }
        }
        
        public IContactRepository Contacts
        {
            get
            {
                if (_contactRepository == null)
                {
                    _contactRepository = new ContactRepository(_dbContext);
                }

                return _contactRepository;
            }
        }

        public IIncidentRepository Incidents
        {
            get
            {
                if (_incidentRepository == null)
                {
                    _incidentRepository = new IncidentRepository(_dbContext);
                }

                return _incidentRepository;
            }
        }

        public void Save()
        {
            _dbContext.SaveChanges();
        }
        
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    _dbContext.Dispose();
                }
            }

            _disposed = true;
        }
    }
}