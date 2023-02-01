using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class ServProvider
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ServName { get; set; }
        public string? Description { get; set; }
        public int? UserId { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<User> Users { get; set; }
        public virtual ServCategory ServCategory { get; set; }
        public virtual ICollection<ContratService> ContratServices { get; set; }
    }
}
