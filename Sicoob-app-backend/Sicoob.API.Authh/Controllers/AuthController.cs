using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Sicoob.API.Authh.Business;
using Sicoob.API.Authh.DTO;
using Sicoob.API.Authh.Model;
using Sicoob.API.Authh.Repository.Interface;

namespace Sicoob.API.Authh.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ILoginRepository _loginRepository;
        private readonly LoginBusiness _loginBusiness;
        private readonly ICadastroRepository _cadastroRepository;

        public AuthController(ILoginRepository loginRepository, LoginBusiness loginBusiness, ICadastroRepository cadastroRepository) 
        {
            _loginRepository = loginRepository;
            _loginBusiness = loginBusiness;
            _cadastroRepository = cadastroRepository;
        }

        [HttpPost("Login")]
        public IActionResult Login(LoginDto user)
        {
            var usuario = _loginBusiness.Login(user);
            return Ok();
        }

        [HttpPost("CadastroUsuario")]
        public async Task<IActionResult> CadastroUsuario(UsuarioCadastro user)
        {
            var resposta = await _cadastroRepository.CadastroUsuario(user);
            return Ok(resposta);
        }

        [HttpGet("GetAllUsers")]
        public async Task<List<UsuarioCadastro>> GetAllUsers()
        {
            var listaUsuarios = await _cadastroRepository.GetAllUsers();
            return listaUsuarios;
        }

        [HttpGet("GetUserByEmail")]
        public async Task<ActionResult<UsuarioCadastro>> GetUserByEmail(string email)
        {
            var usuario = await _cadastroRepository.GetUserByEmail(email);
            if(usuario == null)
                return BadRequest("Usuário não encontrado!");
            else
                return Ok(usuario);
        }

        [HttpPut("UpdateUser")]
        public async Task<ActionResult<UsuarioCadastro>> UpdateUser(UsuarioCadastro user)
        {
            var usuario = await _cadastroRepository.UpdateUser(user);
            if (usuario == null)
                return BadRequest("Não foi possível realizar essa alteração. Fale com um adm do sistema!");
            else
                return Ok(usuario);
        }

        [HttpPost("CadastroLogin")]
        public async Task<ActionResult<Usuario>> CadastroLogin(UsuarioDto user)
        {
            var login = await _loginRepository.CadastroLogin(user);
            if (login.Value == null)
                return BadRequest("Cadastro de Login indisponível. Fale com um adm do sistema!");
            else
                return Ok(login);
        }

        [HttpPatch("UpdateUserSenha")]
        public async Task<ActionResult<string>> UpdateUserSenha(string email, string senhaAtual, string novaSenha)
        {
            var usuario = await _loginRepository.UpdateUserSenha(email, senhaAtual, novaSenha);
            if (usuario == null)
                return BadRequest("Atualização de Senha indisponível. Fale com um adm do sistema!");
            else
                return Ok("Senha alterada com sucesso!");
        }

        [HttpPatch("EsqueciMinhaSenha")]
        public async Task<ActionResult<string>> EsqueciMinhaSenha(string email, string novaSenha)
        {
            var usuario = await _loginRepository.EsqueciMinhaSenha(email, novaSenha);
            if (usuario == null)
                return BadRequest("Alteração de Senha indisponível. Fale com um adm do sistema!");
            else
                return Ok("Senha alterada com sucesso!");
        }

        [HttpPost("EsqueciMinhaSenhaMail")]
        public IActionResult EsqueciMinhaSenhaMail(string email)
        {
            var usuario = _loginBusiness.EsqueciMinhaSenhaMail(email);
            if (usuario) return Ok();
                    return BadRequest("Não foi possível enviar o email de redefinição de senha. Tentar mais tarde!");
        }

        [HttpPost("PrimeiroAcessoMail")]
        public IActionResult PrimeiroAcessoMail(string email)
        {
            var usuario = _loginBusiness.PrimeiroAcessoMail(email);
            if (usuario) return Ok();
            return BadRequest("Não foi possível enviar o email de primeiro acesso. Tentar mais tarde!");
        }
    }
}
