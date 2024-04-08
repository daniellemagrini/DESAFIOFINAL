using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sicoob.API.Auth.DTO;
using Sicoob.API.Auth.Services;

namespace Sicoob.API.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthInterface _authInterface;
        public AuthController(IAuthInterface authInterface) 
        {
            _authInterface = authInterface;
        }

        [HttpPost("Login")]
        public IActionResult Login(UsuarioLoginDto usuarioLogin)
        {
            var resposta = _authInterface.Login(usuarioLogin);
            return Ok(resposta);
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(UsuarioCriacaoDto usuarioRegister)
        {
            var resposta = await _authInterface.Registrar(usuarioRegister);
            return Ok(resposta);
        }
    }
}
