using System.ComponentModel.DataAnnotations;

namespace Uneed_API.Models
{
    public class Rol
    {
        [Key]
        public int Id { get; set; }
        public string? Description { get; set; }
        public string? Status { get; set; }
        public DateTime Add {get; set; }
        public virtual ICollection<User> Users { get; set; }
    }
}
