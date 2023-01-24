using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class ServiceProvider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ProfessionName { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ServiceCategory ServiceCategory { get; set; } 
        public virtual ICollection<ContratService> ContratServices { get; set; } 
    }
}
