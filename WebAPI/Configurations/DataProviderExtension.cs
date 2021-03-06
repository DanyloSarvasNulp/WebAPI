using Microsoft.Extensions.DependencyInjection;
using WebAPI.Repository;
using WebAPI.Repository.Interfaces;

namespace WebAPI.Configurations
{
    public static class DataProviderExtension
    {
        public static void AddDataProvider(this IServiceCollection services)
        {
            services.AddTransient<IContactRepository, ContactRepository>();
            services.AddTransient<IAccountRepository, AccountRepository>();
            services.AddTransient<IIncidentRepository, IncidentRepository>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }
    }
}