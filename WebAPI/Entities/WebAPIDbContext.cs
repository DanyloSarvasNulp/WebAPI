using Microsoft.EntityFrameworkCore;
using WebAPI.Entities.Configurations;
using WebAPI.Entities.Models;

namespace WebAPI.Entities
{
    public class WebApiDbContext : DbContext
    {
        public WebApiDbContext(DbContextOptions<WebApiDbContext> options)
            : base(options)
        {
            
        }
        
        public DbSet<Account> Accounts { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Incident> Incidents { get; set; }

        protected void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AccountConfig());
            modelBuilder.ApplyConfiguration(new ContactConfig());
            modelBuilder.ApplyConfiguration(new IncidentConfig());
            
            base.OnModelCreating(modelBuilder);
        }
    }
}