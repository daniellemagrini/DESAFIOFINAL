using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sicoob.API.AuthOriginal.Data;
using Sicoob.API.AuthOriginal.Model;
using Sicoob.API.AuthOriginal.Repository.Interface;
using System.Globalization;

namespace Sicoob.API.AuthOriginal.Repository
{
    public class LoginRepository : ILoginRepository
    {
        private readonly AppDbContext _context;
        public static UsuarioSistema novoLogin = new UsuarioSistema();

        public LoginRepository(AppDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Método que busca todos os usuários com login cadastrados no sistema.
        /// </summary>  
        [HttpGet]
        public async Task<List<UsuarioSistema>> GetAllUsersLogin()
        {
            try
            {
                return await _context.Login.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar a lista de logins. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método que busca os usuários com login cadastrados no sistema pelo ID de usuário.
        /// </summary>  
        [HttpGet]
        public async Task<UsuarioSistema> GetAllUsersLoginByID(string id)
        {
            try
            {
                return await _context.Login.SingleOrDefaultAsync(x => x.IDUSUARIO == id);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o ID desse login. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método que busca os usuários com login cadastrados no sistema pelo Login.
        /// </summary>  
        [HttpGet]
        public async Task<UsuarioSistema> GetAllUsersLoginByLogin(string login)
        {
            try
            {
                return await _context.Login.SingleOrDefaultAsync(x => x.LOGIN == login);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o login desse usuário. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método para verificar se já existe um cadastro no sistema com esse login.
        /// </summary>
        [HttpGet]
        public bool VerificaLoginExistente(string login)
        {
            try
            {
                if (_context.Login.Any(x => x.LOGIN == login))
                    return true; return false;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível verificar se o login já exite no sistema. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método de envio de email para definição de senha no primeiro acesso.
        /// </summary>
        public bool PrimeiroAcessoMail(string email)
        {
            try
            {
                var usuario = _context.Usuarios.SingleOrDefaultAsync(x => x.DESCEMAIL == email);

                if(usuario != null) return true; return false;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível enviar o email de primeiro acesso. Tente novamente mais tarde!");
            }
        }   
    }
}
