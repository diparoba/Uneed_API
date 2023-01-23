using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class ServiceCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ServiceName { get; set; }
        public virtual ICollection<ServiceProvider> ServiceProviders { get; set; }
    }
}
