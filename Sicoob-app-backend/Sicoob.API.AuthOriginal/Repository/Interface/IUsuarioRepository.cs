using Microsoft.AspNetCore.Mvc;
using Sicoob.API.AuthOriginal.Model;

namespace Sicoob.API.AuthOriginal.Repository.Interface
{
    public interface IUsuarioRepository
    {
        Task<List<Usuario>> GetAllUsers();
        Task<ActionResult<Usuario>> GetUserByID(string id);
        Task<ActionResult<Usuario>> GetUserByCPF(string cpf);
        Task<ActionResult<Usuario>> GetUserByEmail(string email);
        bool VerificaUsuarioExistente(string cpf);
        Task<ActionResult<Usuario>> CadastroUsuario(Usuario user, UsuarioSistema login, IList<int> listaGrupoAcesso);
        Task<ActionResult<Usuario>> UpdateUsuario(Usuario user);

    }
}
