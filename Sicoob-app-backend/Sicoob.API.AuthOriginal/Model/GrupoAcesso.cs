using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicoob.API.AuthOriginal.Model
{
    [Table("03t_GRUPOACESSO")]
    public class GrupoAcesso
    {
        [Display(Name = "ID")]
        [Key]
        public int IDGRUPOACESSO { get; set; }

        [Display(Name = "GRUPO DE ACESSO")]
        public string DESCGRUPOACESSO { get; set; }

        [Display(Name = "CRIADO POR")]
        public string CODCRIADOPOR { get; set; }

        [Display(Name = "DATA CRIACAO")]
        public string DATAHORACRIACAO { get; set; }

        [Display(Name = "ALTERADO POR")]
        public string CODALTERADOPOR { get; set; }

        [Display(Name = "DATA ALTERACAO")]
        public string DATAHORAALTERACAO { get; set; }

        [Display(Name = "INATIVO POR")]
        public string CODINATIVOPOR { get; set; }

        [Display(Name = "DATA INATIVO")]
        public string DATAHORAINATIVO { get; set; }
    }
}
