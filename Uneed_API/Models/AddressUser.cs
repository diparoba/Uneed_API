using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Uneed_API.Models
{
    public class AddressUser
    {
        public int UserId { get; set; }
        public User User { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public virtual ICollection<ContratService> ContratService { get; set; }
    }
}