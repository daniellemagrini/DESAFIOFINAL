using System.ComponentModel.DataAnnotations;

namespace Sicoob.API.Authh.DTO
{
    public class UsuarioDto
    {
        
        public int id { get; set; }

        [Required(ErrorMessage = "O campo email é obrigatório!"), EmailAddress(ErrorMessage = "Email Inválido!")]
        public string email { get; set; }

        [Required(ErrorMessage = "O campo senha é obrigatório!")]
        public string senha { get; set; }

        [Compare("senha", ErrorMessage = "Senhas não conferem!")]
        public string confirmaSenha { get; set; }
    }
}
