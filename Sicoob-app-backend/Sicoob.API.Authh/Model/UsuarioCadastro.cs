using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicoob.API.Authh.Model
{
    [Table("TB_CADASTRO")]
    public class UsuarioCadastro
    {
        [Display(Name = "ID")]
        [Key]
        public int ID_CADASTRO { get; set; }

        [Display(Name = "CPF")]
        public String NR_CPF { get; set; }

        [Display(Name = "NOME COMPLETO")]
        public String NM_PESSOA { get; set; }

        [Display(Name = "DATA DE NASCIMENTO")]
        public String DT_NASCIMENTO { get; set; }

        [Display(Name = "EMAIL")]
        public String DS_EMAIL { get; set; }

        [Display(Name = "DATA DA CRIAÇÃO")]
        public String DT_CRIACAO { get; set; }

        [Display(Name = "DATA DE EXCLUSÃO")]
        public String? DT_EXCLUSAO { get; set; }

        [Display(Name = "LOGRADOURO")]
        public string DS_LOGRADOURO { get; set; }

        [Display(Name = "Nº")]
        public string NR_NUMERO { get; set; }

        [Display(Name = "BAIRRO")]
        public string DS_BAIRRO { get; set; }

        [Display(Name = "MUNICÍPIO")]
        public string NM_MUNICIPIO { get; set; }

        [Display(Name = "UF")]
        public string NM_UF { get; set; }

        [Display(Name = "CEP")]
        public string NR_CEP { get; set; }

        //ForeignKeys
        [Display(Name = "UNIDADE")]
        [ForeignKey("UnidadeCadastro")]
        public int ID_UNIDADE { get; set; }

        [Display(Name = "INSTITUIÇÃO")]
        [ForeignKey("InstituicaoCadastro")]
        public int ID_INSTITUICAO { get; set; }

        [Display(Name = "ID DE LOGIN")]
        [ForeignKey("Usuario")]
        public int ID_USUARIO { get; set; }
    }
}
