using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string? ServiceName { get; set; }
        public string? Status { get; set; }
        public virtual ICollection<Provider> Providers { get; set; }
    }
}
