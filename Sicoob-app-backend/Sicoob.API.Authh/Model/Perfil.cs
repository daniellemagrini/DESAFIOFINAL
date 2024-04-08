using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sicoob.API.Authh.Model
{
    [Table("TB_PERFIL")]
    public class Perfil
    {
        [Key]
        public int id { get; set; }
        public string dsPerfil { get; set; }
        public IList<Usuario> listaUsuarios { get; set; }
    }
}
