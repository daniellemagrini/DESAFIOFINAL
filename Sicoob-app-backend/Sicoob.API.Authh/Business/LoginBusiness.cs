using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sicoob.API.Authh.DTO;
using Sicoob.API.Authh.Helpers;
using Sicoob.API.Authh.Model;
using Sicoob.API.Authh.Repository.Interface;
using Sicoob.Mail;

namespace Sicoob.API.Authh.Business
{
    public class LoginBusiness
    {
        private readonly ILoginRepository _loginRepository;
        private readonly ISenhaHash _senhaHash;
        public LoginBusiness(ILoginRepository loginRepository, ISenhaHash senhaHash)
        {
            _loginRepository = loginRepository;
            _senhaHash = senhaHash;
        }

        public Usuario Login(LoginDto user)
        {
            var usuario = _loginRepository.Login(user);

            if (usuario != null) 
            {

                if (_senhaHash.VerificaSenhaHash(usuario.ID_USUARIO, user.senha, usuario.HS_SENHA, usuario.DS_SENHA))
                {
                    EnviarCodigoVerificacao(usuario);
                }   
            }

            return usuario; 
        }

        public bool EsqueciMinhaSenhaMail(string email)
        {
            var usuario = _loginRepository.EsqueciMinhaSenhaMail(email);

            if (usuario)
            {                
                EnviarEmailRedefinirSenha(email);
            }

            return usuario;
        }

        public bool PrimeiroAcessoMail(string email)
        {
            var usuario = _loginRepository.PrimeiroAcessoMail(email);

            if (usuario)
            {
                EnviarEmailPrimeiroAcessoa(email);
            }

            return usuario;
        }

        /// <summary>
        /// Método que envia email com código de verificação.
        /// </summary>
        private void EnviarCodigoVerificacao(Usuario usuario)
        {
            var codigo = GerarCodigoVerificacao();
            var assunto = "Código de Verificação Sicoob";
            var corpoEmail = "<br><br><h4>Seu código de verificação é:</h4>" + "<h2><strong>"
                    + codigo + "</strong></h2><br><br>" +             
                    "<h3>Time Sicoob</h3>";

            Mail.Mail.SendEmail(usuario.DS_EMAIL, assunto, corpoEmail);
        }

        /// <summary>
        /// Método que envia email de redefinição de senha.
        /// </summary>
        private void EnviarEmailRedefinirSenha(string email)
        {
            var destinatario = email;
            var assunto = "Redefinir Senha - Sicoob";
            var corpoEmail = "<br><br><h4>Clique no link abaixo para redefinir sua senha</h4><h2>" +
                             "<a href='https://www.w3schools.com/html/tryit.asp?filename=tryhtml_editor' style = 'border: 1px solid rgba(245, 245, 245, 1);" +
                             "background: rgba(0, 60, 68, 1); color: rgba(245, 245, 245, 1);" +
                             "padding: .7rem; border-radius: .5rem; font-weight: 600;'>REDEFINIR SENHA</a>" +
                             "</h2><br><br><h3>Time Sicoob</h3>";

            Mail.Mail.SendEmail(destinatario, assunto, corpoEmail);
        }

        /// <summary>
        /// Método que envia email de primeiro acesso.
        /// </summary>
        private void EnviarEmailPrimeiroAcessoa(string email)
        {
            var destinatario = email;
            var assunto = "Primeiro Acesso - Sicoob";
            var corpoEmail = "<br><br><h4>Clique no link abaixo para redefinir sua senha</h4><h2>" +
                             "<a href='https://www.w3schools.com/html/tryit.asp?filename=tryhtml_editor' style = 'border: 1px solid rgba(245, 245, 245, 1);" +
                             "background: rgba(0, 60, 68, 1); color: rgba(245, 245, 245, 1);" +
                             "padding: .7rem; border-radius: .5rem; font-weight: 600;'>DEFINIR SENHA</a>" +
                             "</h2><br><br><h3>Time Sicoob</h3>";

            Mail.Mail.SendEmail(destinatario, assunto, corpoEmail);
        }

        /// <summary>
        /// Método que faz a geração de um código aleatório para ser usado como código de verificação.
        /// </summary>
        public string GerarCodigoVerificacao()
        {
            Random random = new Random();
            List<int> list = new List<int>();
            var codigo = string.Empty;
            var codigoVerificacao = string.Empty;

            try
            {
                for (int i = 0; i < 6; i++)
                {
                    var num = random.Next(0, 10);
                    list.Add(num);
                }

                if (list.Count > 0)
                {
                    foreach (int n in list)
                    {
                        codigoVerificacao = codigoVerificacao + Convert.ToString(n);
                    }

                    return codigoVerificacao;
                }
                else
                {
                    throw new Exception("Não foi possível gerar código de verificação.");
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Não foi possível gerar código de verificação.");
            }
        }
    }
}
