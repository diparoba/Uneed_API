using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class ContratService
    {
        [Key] 
        public int Id { get; set; }
        public DateTime DayDate { get; set; }
        public DateTime CreateDate { get; set; }
        public string? Direction { get; set; }
        public string? Finish { get; set; }
        public string? State { get; set; }
        public virtual User User { get; set; }
        public virtual ServiceProvider ServiceProvider { get; set; }
        public virtual ICollection<Calification> Califications { get; set; } 
    }
}
