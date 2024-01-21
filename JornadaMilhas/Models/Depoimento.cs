using System.ComponentModel.DataAnnotations;

namespace Jornada_Milhas.Models
{
    public class Depoimento
    {
        [Key]
        public int Id { get; set; }
        public string Foto { get; set; }
        [Required]
        public string Comentario { get; set; }
        [Required]
        public string NomeUsuario { get; set;}
        public bool Deleted { get; set; }
        public DateTime? DeletedDate { get; set; }

    }
}
