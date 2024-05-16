using System.ComponentModel.DataAnnotations;

namespace Sicoob.API.AuthOriginal.DTO
{
    public class UsuarioSistemaDTO
    {
        [Required(ErrorMessage = "O campo login é obrigatório!")]
        public string LOGIN { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        public string PASSWORD { get; set; }
    }
}
