using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicoob.API.Authh.Model
{
    [Table("TB_USUARIO")]
    public class Usuario
    {
        [Display(Name = "ID")]
        [Key]
        public int ID_USUARIO { get; set; }

        [Display(Name = "EMAIL DE LOGIN")]
        public string DS_EMAIL { get; set; }

        [Display(Name = "SENHA SALT")]
        public byte[] DS_SENHA { get; set;}

        [Display(Name = "SENHA HASH")]
        public byte[] HS_SENHA { get; set; }

        public enum ID_STATUS { }

        [Display(Name = "PERFIL")]
        public IList<Perfil> listaPerfil { get; set; }
    }
}
