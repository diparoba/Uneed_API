using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class Provider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ServName { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public virtual User User { get; set; }
        public int UserId{get; set;}
        public virtual Category Category { get; set; }
        public virtual ICollection<ContratService> ContratServices { get; set; }
    }
}
