using Microsoft.AspNetCore.Mvc;
using Sicoob.API.Authh.Model;

namespace Sicoob.API.Authh.Repository.Interface
{
    public interface ICadastroRepository
    {
        Task<ActionResult<UsuarioCadastro>> CadastroUsuario(UsuarioCadastro user);
        Task<List<UsuarioCadastro>> GetAllUsers();
        Task<ActionResult<UsuarioCadastro>> GetUserByEmail(string email);
        Task<ActionResult<UsuarioCadastro>> UpdateUser(UsuarioCadastro user);
    }
}
