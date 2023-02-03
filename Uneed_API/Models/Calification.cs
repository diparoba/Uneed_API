using System.ComponentModel.DataAnnotations;
namespace Uneed_API.Models
{
    public class Calification
    {
        [Key] public int Id { get; set; }
        public Int16 Stars { get; set; }
        public string? Commentary { get; set; }
        public virtual ContratService ContratService { get; set; }
    }
}
