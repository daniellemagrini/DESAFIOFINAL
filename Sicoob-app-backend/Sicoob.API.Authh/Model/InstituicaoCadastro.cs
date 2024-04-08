namespace Sicoob.API.Authh.Model
{
    public class InstituicaoCadastro
    {
        public int ID_INSTITUICAO { get; set; }
        public string NR_CNPJ { get; set; }
        public string DS_INSTITUICAO { get; set; }
        public DateOnly DT_CRIACAO { get; set; }
        public DateOnly DT_EXCLUSAO { get; set; }
        public string NR_INSCRICAO_MUNUCIPÁL { get; set; }
        public string NR_NIRE { get; set; }
    }
}
