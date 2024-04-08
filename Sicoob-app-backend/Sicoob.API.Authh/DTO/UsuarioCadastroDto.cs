using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Sicoob.API.Authh.DTO
{
    public class UsuarioCadastroDto
    {
        [Key]
        public String NR_CPF { get; set; }
        public String NM_PESSOA { get; set; }
        public String DT_NASCIMENTO { get; set; }
        public String DS_EMAIL { get; set; }
        public String DT_CRIACAO { get; set; }
        public string DS_LOGRADOURO { get; set; }
        public string NR_NUMERO { get; set; }
        public string DS_BAIRRO { get; set; }
        public string NM_MUNICIPIO { get; set; }
        public string NM_UF { get; set; }
        public string NR_CEP { get; set; }

        //ForeignKeys
        [ForeignKey("UnidadeCadastro")]
        public int ID_UNIDADE { get; set; }

        [ForeignKey("InstituicaoCadastro")]
        public int ID_INSTITUICAO { get; set; }
    }
}
