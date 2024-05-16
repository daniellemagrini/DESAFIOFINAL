using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Ocsp;
using Sicoob.API.AuthOriginal.Model;
using Sicoob.API.AuthOriginal.Repository.Interface;

namespace Sicoob.API.AuthOriginal.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UserController(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        [HttpGet("GetAllUsers")]
        public async Task<List<Usuario>> GetAllUsers()
        {
            var resposta = await _usuarioRepository.GetAllUsers();
            return resposta;
        }

        [HttpGet("GetUserByID")]
        public async Task<IActionResult> GetUserByID(string id)
        {
            var resposta = await _usuarioRepository.GetUserByID(id);
            return Ok(resposta);
        }

        [HttpGet("GetUserByCPF")]
        public async Task<IActionResult> GetUserByCPF(string cpf)
        {
            var resposta = await _usuarioRepository.GetUserByCPF(cpf);
            return Ok(resposta);
        }

        [HttpGet("GetUserByEmail")]
        public async Task<IActionResult> GetUserByEmail(string email)
        {
            var resposta = await _usuarioRepository.GetUserByEmail(email);
            return Ok(resposta);
        }

        [HttpGet("VerificaUsuarioExistente")]
        public bool VerificaUsuarioExistente(string cpf)
        {
            var resposta = _usuarioRepository.VerificaUsuarioExistente(cpf);
            return resposta;
        }

        [HttpPost("CadastroUsuario")]
        public async Task<IActionResult> CadastroUsuario([FromQuery] Usuario user, [FromBody] UsuarioSistema login, [FromQuery] IList<int> listaGrupoAcesso)
        {
            var resposta = await _usuarioRepository.CadastroUsuario(user, login, listaGrupoAcesso);
            return Ok(resposta);
        }

        [HttpPost("UpdateUsuario")]
        public async Task<IActionResult> UpdateUsuario(Usuario user)
        {
            var resposta = await _usuarioRepository.UpdateUsuario(user);
            return Ok(resposta);
        }
    }
}