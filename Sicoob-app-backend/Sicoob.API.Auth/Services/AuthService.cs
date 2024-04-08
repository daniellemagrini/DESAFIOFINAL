using Microsoft.EntityFrameworkCore;
using Sicoob.API.Auth.Data;
using Sicoob.API.Auth.DTO;
using Sicoob.API.Auth.Models;

namespace Sicoob.API.Auth.Services
{
    public class AuthService : IAuthInterface
    {
        private readonly AppDbContext _appDbContext;
        private readonly ISenhaInterface _senhaInterface;
        public AuthService(AppDbContext context, ISenhaInterface senhaInterface) 
        {
            _appDbContext = context;
            _senhaInterface = senhaInterface;   
        }

        public async Task<Response<UsuarioCriacaoDto>> Registrar (UsuarioCriacaoDto usuarioRegistro)
        {
            Response<UsuarioCriacaoDto> respostaService = new Response<UsuarioCriacaoDto>();

            try
            {
                if (!VerificaEmailUsuarioExiste(usuarioRegistro)) 
                {
                    respostaService.Dados = null;
                    respostaService.Mensagem = "Usuário já cadastrado!";
                    respostaService.Status = false;

                    return respostaService;
                }

                _senhaInterface.CriarSenhaHash(usuarioRegistro.senha, out byte[] senhaHash, out byte[] senhaSalt);

                UsuarioModel usuario = new UsuarioModel()
                {
                    email = usuarioRegistro.email,
                    senhaHash = senhaHash,
                    senhaSalt = senhaSalt
                };

                _appDbContext.Add(usuario);
                await _appDbContext.SaveChangesAsync();

                respostaService.Mensagem = "Usuário criado com sucesso!";
            }
            catch (Exception ex)
            {
                respostaService.Dados = null;
                respostaService.Mensagem = ex.Message;
                respostaService.Status = false;
            }

            return respostaService;
        }

        public bool VerificaEmailUsuarioExiste(UsuarioCriacaoDto usuarioRegistro)
        {
            var usuario = _appDbContext.TBLOGIN.FirstOrDefault(x => x.email == usuarioRegistro.email);

            if (usuario != null) return false;

            return true;
        }

        public async Task<Response<string>> Login(UsuarioLoginDto usuarioLogin)
        {
            Response<string> respostaServico = new Response<string>();
            try
            {
                var usuario = await _appDbContext.TBLOGIN.FirstOrDefaultAsync(userBanco => userBanco.email == usuarioLogin.Email);

                if (usuario == null)
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                if (!_senhaInterface.VerificaSenhaHash(usuarioLogin.Senha, usuario.senhaHash, usuario.senhaSalt))
                {
                    respostaServico.Mensagem = "Credenciais inválidas!";
                    respostaServico.Status = false;
                    return respostaServico;
                }

                var token = _senhaInterface.CriarToken(usuario);

                respostaServico.Dados = token;
                respostaServico.Mensagem = "Usuário logado com sucesso!";
            }
            catch (Exception ex)
            {
                respostaServico.Dados = null;
                respostaServico.Mensagem = ex.Message;
                respostaServico.Status = false;
            }

            return respostaServico;
        }
    }
}
