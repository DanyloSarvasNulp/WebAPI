using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Entities.Models
{
    public class Incident
    {
        public virtual Account Account { get; set; }
        public int AccountId { get; set; }
        
        // кастиль, ібо я не знаю як в fluent api
        // зробити string головним ключем
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        // [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Name { get; set; }
        public string Description { get; set; }
    }
}