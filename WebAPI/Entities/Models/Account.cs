using System.Collections.Generic;

namespace WebAPI.Entities.Models
{
    public class Account : IEntity
    {
        public List<Incident> Incidents { get; set; }
        public virtual Contact Contact { get; set; }
        public int ContactId { get; set; }

        public int Id { get; set; }
        public string Name { get; set; }
    }
}