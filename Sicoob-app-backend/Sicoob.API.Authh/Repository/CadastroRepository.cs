using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sicoob.API.Authh.Data;
using Sicoob.API.Authh.DTO;
using Sicoob.API.Authh.Helpers;
using Sicoob.API.Authh.Model;
using Sicoob.API.Authh.Repository.Interface;
using System.Globalization;

namespace Sicoob.API.Authh.Repository
{
    public class CadastroRepository : ICadastroRepository
    {
        private readonly AppDbContext _context;
        private readonly ISenhaHash _senhaHash;
        private readonly ILoginRepository _loginRepository;
        public static UsuarioCadastro novoUsuario = new UsuarioCadastro();
        public static Usuario novoLogin = new Usuario();

        public CadastroRepository(AppDbContext context, ISenhaHash senhaHash, ILoginRepository loginRepository)
        {
            _context = context;
            _senhaHash = senhaHash;
            _loginRepository = loginRepository;
        } 

        /// <summary>
        /// Método para cadastrar um usuário.
        /// </summary>
        public async Task<ActionResult<UsuarioCadastro>> CadastroUsuario(UsuarioCadastro user)
        {
            try
            {
                bool existeLogin = _loginRepository.VerificaLogin(user.DS_EMAIL);

                if (!existeLogin)
                {
                    novoUsuario.NR_CPF = user.NR_CPF;
                    novoUsuario.NM_PESSOA = user.NM_PESSOA;
                    novoUsuario.DT_NASCIMENTO = user.DT_NASCIMENTO;
                    novoUsuario.DS_EMAIL = user.DS_EMAIL;
                    novoUsuario.DT_CRIACAO = DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("pt-br"));
                    novoUsuario.DS_LOGRADOURO = user.DS_LOGRADOURO;
                    novoUsuario.NR_NUMERO = user.NR_NUMERO;
                    novoUsuario.DS_BAIRRO = user.DS_BAIRRO;
                    novoUsuario.NM_MUNICIPIO = user.NM_MUNICIPIO;
                    novoUsuario.NM_UF = user.NM_UF;
                    novoUsuario.ID_UNIDADE = user.ID_UNIDADE;
                    novoUsuario.ID_INSTITUICAO = user.ID_INSTITUICAO;

                    await _context.UsuariosCadastro.AddAsync(user);
                    await _context.SaveChangesAsync();
                }
                return novoUsuario;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível finalizar o cadastro do usuário. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método que busca todos os usuários cadastrados.
        /// </summary>
        [HttpGet]
        public async Task<List<UsuarioCadastro>> GetAllUsers()
        {
            try
            {
                return await _context.UsuariosCadastro.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar a lista de usuários. Tente novamente mais tarde!");
            }

        }

        /// <summary>
        /// Método que busca um usuário pelo seu email.
        /// </summary>
        public async Task<ActionResult<UsuarioCadastro>> GetUserByEmail(string email)
        {
            try
            {
                var usuario = await _context.UsuariosCadastro.SingleOrDefaultAsync(x => x.DS_EMAIL == email);
                return usuario;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o usuário solicitado. Tente novamente mais tarde!");
            }

        }

        /// <summary>
        /// Método que atualiza o cadastro de um usuário.
        /// </summary>
        public async Task<ActionResult<UsuarioCadastro>> UpdateUser(UsuarioCadastro user)
        {
            try
            {
                var usuario = await _context.UsuariosCadastro.FindAsync(user.ID_CADASTRO);
                if (usuario != null)
                {
                    usuario.NR_CPF = user.NR_CPF;
                    usuario.NM_PESSOA = user.NM_PESSOA;
                    usuario.DT_NASCIMENTO = user.DT_NASCIMENTO;
                    usuario.DS_EMAIL = user.DS_EMAIL;
                    usuario.DT_CRIACAO = DateTime.Now.ToString(CultureInfo.CreateSpecificCulture("pt-br"));
                    usuario.DS_LOGRADOURO = user.DS_LOGRADOURO;
                    usuario.NR_NUMERO = user.NR_NUMERO;
                    usuario.DS_BAIRRO = user.DS_BAIRRO;
                    usuario.NM_MUNICIPIO = user.NM_MUNICIPIO;
                    usuario.NM_UF = user.NM_UF;
                    usuario.ID_UNIDADE = user.ID_UNIDADE;
                    usuario.ID_INSTITUICAO = user.ID_INSTITUICAO;

                    var existeLogin = _loginRepository.VerificaLogin(user.DS_EMAIL);

                    if (!existeLogin)
                    {
                        //await _context.UsuariosCadastro.Where(u => u.ID_CADASTRO == user.ID_CADASTRO).ExecuteUpdateAsync(p => p.SetProperty<Usuario>();
                        await _context.SaveChangesAsync();
                    }
                }
                return novoUsuario;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível finalizar o cadastro do usuário. Tente novamente mais tarde!");
            }
        }
    }
}
