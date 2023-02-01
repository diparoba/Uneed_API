using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class ServCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ServiceName { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<ServProvider> ServProviders { get; set; }
    }
}
