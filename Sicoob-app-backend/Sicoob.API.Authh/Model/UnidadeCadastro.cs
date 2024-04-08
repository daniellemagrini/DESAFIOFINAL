using System.ComponentModel.DataAnnotations.Schema;

namespace Sicoob.API.Authh.Model
{
    public class UnidadeCadastro
    {
        public int ID_UNIDADE { get; set; }
        public string DS_UNIDADE { get; set; }
        public string NR_CNPJ { get; set; }
        public string DS_SIGLA { get; set; }
        public DateOnly DT_CRIACAO { get; set; }
        public DateOnly DT_EXCLUSAO { get; set; }
        public string NR_INSCRICAO_MUNICIPAL { get; set; }
        public string NR_NIRE { get; set; }
        
        //Foreign key     
        public int ID_INSTITUICAO { get; set; }

        [ForeignKey("ID_INSTITUICAO")]
        public InstituicaoCadastro InstituicaoCadastro { get; set; }
    }
}
