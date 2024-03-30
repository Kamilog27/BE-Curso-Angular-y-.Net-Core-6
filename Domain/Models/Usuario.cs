using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BackEndCuestionario.Domain.Models
{
    public class Usuario
    {
        public int Id { get; set; }
        [Required]
        [Column(TypeName ="varchar(20)")]
        public string NombreUsuario { get; set; }
        [Required]
        [Column(TypeName = "varchar(50)")]
        public string Contraseña { get; set; }
    }
}
