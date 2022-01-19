using System.Collections.Generic;

namespace WebAPI.Entities.Models
{
    public class Contact : IEntity
    {
        public List<Account> Accounts { get; set; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
    }
}