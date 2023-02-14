using System.ComponentModel.DataAnnotations;

namespace Uneed_API.Models
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Lastname { get; set; }
        public string? Status { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public string? Identification { get; set; }
        public string? Phone { get; set; }
        public string? Adress { get; set; }
        public bool? IsProvider { get; set; }
        public string? Gender { get; set; }
        public DateTime BirthDate { get; set; }
        public int RolId { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime CreatedDate { get; set; }
        public virtual Rol Rol { get; set; }
        public virtual ICollection<Provider> Provider { get; set; }
        public virtual ICollection<ContratService> ContratService { get; set; }

    }
}
