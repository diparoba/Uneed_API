using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class Address
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set;}
        public string? PrincipalStreet { get; set; }
        public string? SecondaryStreet { get; set; }
        public string? City { get; set; }
        public virtual ICollection<AddressUser> AddressUser { get; set; }
    }
}
