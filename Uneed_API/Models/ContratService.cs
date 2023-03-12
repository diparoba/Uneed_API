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
        public DateTime Finish { get; set; }
        public string? State { get; set; }
        public decimal Price { get; set; }
        public int ProviderId { get; set; }
        public virtual User User { get; set; }
        public virtual Provider Provider { get; set; }
        public virtual AddressUser AddressUser { get; set; }
        public virtual ICollection<Calification> Calification { get; set; }
    }
}
