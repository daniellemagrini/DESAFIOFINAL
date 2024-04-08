using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicoob.API.Auth.Models
{
    [Table("TBLOGIN")]
    public class UsuarioModel
    {
        [Key]
        public int idLogin { get; set; }

        [ForeignKey("idUsuario")]
        public int idUsuario { get; set; }

        [Required]
        public string email { get; set; }

        [Required]
        public byte[] senhaHash { get; set; }

        [Required]
        public byte[] senhaSalt { get; set; }
    }
}
