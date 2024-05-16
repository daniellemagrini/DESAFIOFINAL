using System.ComponentModel.DataAnnotations;

namespace Sicoob.API.AuthOriginal.DTO
{
    public class UsuarioDTO
    {
        [Required(ErrorMessage = "O campo ID é obrigatório!")]
        public string IDUSUARIO { get; set; }

        [Required(ErrorMessage = "O campo Unidade de Instituição é obrigatório!")]
        public string IDUNIDADEINST { get; set; }

        [Required(ErrorMessage = "O campo Instituição é obrigatório!")]
        public int IDINSTITUICAO { get; set; }

        [Required(ErrorMessage = "O campo Nome é obrigatório!")]
        public string DESCNOMEUSUARIO { get; set; }

        [Required(ErrorMessage = "O campo CPF é obrigatório!")]
        public string CPFUSUARIO { get; set; }

        [Required(ErrorMessage = "O campo Email é obrigatório!")]
        public string DESCEMAIL { get; set; }

        public int CODCRIADOPOR { get; set; }

        public string DATAHORACRIACAO { get; set; }
    }
}
