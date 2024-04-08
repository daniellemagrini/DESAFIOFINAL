using Microsoft.AspNetCore.Mvc;
using Sicoob.API.Authh.DTO;
using Sicoob.API.Authh.Model;

namespace Sicoob.API.Authh.Repository.Interface
{
    public interface ILoginRepository
    {
        bool VerificaLogin(string username);
        Usuario Login(LoginDto user);
        Task<ActionResult<Usuario>> CadastroLogin(UsuarioDto user);
        Task<int> BuscaIDLoginPeloEmail(string email);
        Task<ActionResult<string>> UpdateUserSenha(string email, string senhaAtual, string novaSenha);
        Task<ActionResult<string>> EsqueciMinhaSenha(string email, string novaSenha);
        bool EsqueciMinhaSenhaMail(string email);
        bool PrimeiroAcessoMail(string email);
    }
}
