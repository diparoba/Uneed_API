using System;
using System.ComponentModel.DataAnnotations;

namespace Uneed_API.Models
{
    public class ContratResponse
    {
        public int Id { get; set; }
        [Required]
        public int UserId { get; set; }

        public int ProviderId { get; set; }

        public DateTime DayDate { get; set; }

        public decimal Price { get; set; }
        public string? State { get; set; }

        public int AddressId { get; set; }
        public string? AddressPrincipalStreet { get; set; }
        public string? AddressSecondaryStreet { get; set; }
        public string? AddressCity { get; set; }
    }
}
