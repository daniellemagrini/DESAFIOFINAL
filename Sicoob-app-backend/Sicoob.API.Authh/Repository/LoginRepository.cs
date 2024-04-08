using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sicoob.API.Authh.Data;
using Sicoob.API.Authh.DTO;
using Sicoob.API.Authh.Helpers;
using Sicoob.API.Authh.Model;
using Sicoob.API.Authh.Repository.Interface;
using System.Data;

namespace Sicoob.API.Authh.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        private readonly ISenhaHash _senhaHash;
        public static UsuarioCadastro novoUsuario = new UsuarioCadastro();
        public static Usuario novoLogin = new Usuario(); 

        public LoginRepository(AppDbContext context, ISenhaHash senhaHash)
        {
            _context = context;
            _senhaHash = senhaHash;
        }

        /// <summary>
        /// Método que verifica se existe o login.
        /// </summary>
        public bool VerificaLogin(string username) =>     
            _context.Usuarios.Any(x => x.DS_EMAIL == username);

        /// <summary>
        /// Método para realizar o login.
        /// </summary>
        public Usuario Login(LoginDto user)
        {
            Usuario usuario = null;

            try
            {
               usuario = _context.Usuarios.SingleOrDefault(userBanco =>
                    userBanco.DS_EMAIL == user.email);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível realizar o login. Tente novamente mais tarde!");
            }

            return usuario;
        }

        /// <summary>
        /// Método que cria um login e senha.
        /// </summary>
        public async Task<ActionResult<Usuario>> CadastroLogin(UsuarioDto user)
        {
            try
            {
                var usuario = await _context.UsuariosCadastro.SingleOrDefaultAsync(x => x.DS_EMAIL == user.email);
                var login = _context.Usuarios.Any(x => x.DS_EMAIL == user.email);

                if (usuario != null && !login)
                {
                    _senhaHash.CriarSenhaHash(user.senha, out byte[] senhaHash, out byte[] senhaSalt);

                    novoLogin.DS_EMAIL = user.email;
                    novoLogin.HS_SENHA = senhaHash;
                    novoLogin.DS_SENHA = senhaSalt;

                    await _context.Usuarios.AddAsync(novoLogin);
                    await _context.SaveChangesAsync();

                    var id = await BuscaIDLoginPeloEmail(user.email);

                    await _context.UsuariosCadastro.Where(u => u.DS_EMAIL == user.email).ExecuteUpdateAsync(p => p.SetProperty(pr => pr.ID_USUARIO, id));
                    await _context.SaveChangesAsync();
                }
                
                return novoLogin;
            }
            catch (Exception)
            {
                _context.Usuarios.Where(a => a.DS_EMAIL == user.email).ExecuteDelete();
                await _context.SaveChangesAsync();
                throw new Exception("Não foi possível criar seu login. Tente novamente mais tarde!");               
            }
        }

        /// <summary>
        /// Método que busca o ID do cadastro pelo email.
        /// </summary>
        public async Task<int> BuscaIDLoginPeloEmail(string email)
        {
            try
            {
                var idLogin = await _context.Usuarios
                    .Where(x => x.DS_EMAIL == email)
                    .Select(x => x.ID_USUARIO)
                    .SingleOrDefaultAsync();

                return idLogin; 
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível encontrar o ID referente a este email.");
            }
        }

        /// <summary>
        /// Método que atualiza a senha de um usuário.
        /// </summary>
        public async Task<ActionResult<string>> UpdateUserSenha(string email, string senhaAtual, string novaSenha)
        {
            try
            {
                var id = await BuscaIDLoginPeloEmail(email);

                if (id != null)
                {
                    var senhaSalt = await _context.Usuarios
                    .Where(x => x.ID_USUARIO == id)
                    .Select(x => x.DS_SENHA)
                    .SingleOrDefaultAsync();

                    var senhaHash = await _context.Usuarios
                    .Where(x => x.ID_USUARIO == id)
                    .Select(x => x.HS_SENHA)
                    .SingleOrDefaultAsync();

                    if (_senhaHash.VerificaSenhaHash(id, senhaAtual, senhaHash, senhaSalt))
                    {
                        _senhaHash.CriarSenhaHash(novaSenha, out byte[] novaSenhaHash, out byte[] novaSenhaSalt);

                        await _context.Usuarios
                                 .Where(u => u.ID_USUARIO == id)
                                 .ExecuteUpdateAsync(p => p
                                     .SetProperty(pr => pr.DS_SENHA, novaSenhaSalt)
                                     .SetProperty(pr => pr.HS_SENHA, novaSenhaHash));

                        await _context.SaveChangesAsync();
                    }                    
                }
                return email;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível alterar a senha. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método Esqueci minha senha.
        /// </summary>
        public async Task<ActionResult<string>> EsqueciMinhaSenha(string email, string novaSenha)
        {
            try
            {
                var id = await BuscaIDLoginPeloEmail(email);

                if (id != null)
                {
                    var senhaSalt = await _context.Usuarios
                    .Where(x => x.ID_USUARIO == id)
                    .Select(x => x.DS_SENHA)
                    .SingleOrDefaultAsync();

                    var senhaHash = await _context.Usuarios
                    .Where(x => x.ID_USUARIO == id)
                    .Select(x => x.HS_SENHA)
                    .SingleOrDefaultAsync();

                    
                    _senhaHash.CriarSenhaHash(novaSenha, out byte[] novaSenhaHash, out byte[] novaSenhaSalt);

                    await _context.Usuarios
                                 .Where(u => u.ID_USUARIO == id)
                                 .ExecuteUpdateAsync(p => p
                                     .SetProperty(pr => pr.DS_SENHA, novaSenhaSalt)
                                     .SetProperty(pr => pr.HS_SENHA, novaSenhaHash));

                    await _context.SaveChangesAsync();
                    
                }
                return email;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível alterar a senha. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método de envio de email para Esqueci minha senha.
        /// </summary>
        public bool EsqueciMinhaSenhaMail(string email)
        {
            var existeUsuario = false;

            try
            {
                existeUsuario = _context.Usuarios.Any(x => x.DS_EMAIL == email);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível encontrar o login. Tente novamente mais tarde!");
            }

            return existeUsuario;
        }

        /// <summary>
        /// Método de envio de email para definição de senha no primeiro acesso.
        /// </summary>
        public bool PrimeiroAcessoMail(string email)
        {
            var existeCadastro = false;
            var existeLogin = false;
            var primeiroAcesso = false;

            try
            {
                existeCadastro = _context.UsuariosCadastro.Any(x => x.DS_EMAIL == email);
                existeLogin = _context.Usuarios.Any(x => x.DS_EMAIL == email);

                if(!existeCadastro && !existeLogin) primeiroAcesso = true;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível enviar o email de primeiro acesso. Tente novamente mais tarde!");
            }

            return primeiroAcesso;
        }
    }
}
