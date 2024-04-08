using Sicoob.API.Auth.DTO;
using Sicoob.API.Auth.Models;

namespace Sicoob.API.Auth.Services
{
    public interface IAuthInterface
    {
        Task<Response<UsuarioCriacaoDto>> Registrar(UsuarioCriacaoDto usuarioRegistro);
        Task<Response<string>> Login(UsuarioLoginDto usuarioLogin);
    }
}
