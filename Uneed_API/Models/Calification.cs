using System.ComponentModel.DataAnnotations;

namespace Uneed_API.Models
{
    public class Calification
    {
        [Key]
        public int Id { get; set; }
        public int Value { get; set; } // valor de la calificación
        public string Comment { get; set; } // comentario de la calificación
        public DateTime Date { get; set; } // fecha de la calificación
        public virtual User User { get; set; } // usuario que realizó la calificación
        public virtual ContratService ContratService { get; set; }
    }
}
