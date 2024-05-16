using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using Sicoob.API.AuthOriginal.Business;
using Sicoob.API.AuthOriginal.Model;
using Sicoob.API.AuthOriginal.Repository.Interface;

namespace Sicoob.API.AuthOriginal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly ILoginRepository _loginRepository;
        private readonly LoginBusiness _loginBusiness;

        public AuthController(IUsuarioRepository usuarioRepository, ILoginRepository loginRepository, LoginBusiness loginBusiness)
        {
            _usuarioRepository = usuarioRepository;
            _loginRepository = loginRepository;
            _loginBusiness = loginBusiness;
        }

        [HttpGet("GetAllUsersLogin")]
        public async Task<List<UsuarioSistema>> GetAllUsersLogin()
        {
            var resposta = await _loginRepository.GetAllUsersLogin();
            return resposta;
        }

        [HttpGet("GetAllUsersLoginByID")]
        public async Task<IActionResult> GetAllUsersLoginByID(string id)
        {
            var resposta = await _loginRepository.GetAllUsersLoginByID(id);
            return Ok(resposta);
        }

        [HttpGet("GetAllUsersLoginByLogin")]
        public async Task<IActionResult> GetAllUsersLoginByLogin(string login)
        {
            var resposta = await _loginRepository.GetAllUsersLoginByLogin(login);
            return Ok(resposta);
        }

        [HttpGet("VerificaLoginExistente")]
        public bool VerificaLoginExistente(string login)
        {
            var resposta = _loginRepository.VerificaLoginExistente(login);
            return resposta;
        }
    }
}