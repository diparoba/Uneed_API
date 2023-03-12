using System.ComponentModel.DataAnnotations;

namespace Uneed_API.Models
{
    public class AddressUser
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public virtual ICollection<ContratService> ContratService { get; set; }
    }
}