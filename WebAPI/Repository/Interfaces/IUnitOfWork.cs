namespace WebAPI.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IContactRepository Contacts { get; }
        IAccountRepository Accounts { get; }
        IIncidentRepository Incidents { get; }

        void Save();
    }
}