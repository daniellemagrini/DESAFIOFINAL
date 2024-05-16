using AceleraPlenoProjetoFinal.Api.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sicoob.API.AuthOriginal.Business;
using Sicoob.API.AuthOriginal.Data;
using Sicoob.API.AuthOriginal.Model;
using Sicoob.API.AuthOriginal.Repository.Interface;

namespace Sicoob.API.AuthOriginal.Repository
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        private readonly ILoginRepository _loginRepository;
        private readonly LoginBusiness _loginBusiness;
        private readonly IOTPGenerateRepository _oTPGenerateRepository;
        public static Usuario novoUsuario = new Usuario();
        public static UsuarioSistema novoLogin = new UsuarioSistema();
        public static UsuarioSistemaGrupoAcesso novoGrupoAcesso = new UsuarioSistemaGrupoAcesso();

        public UsuarioRepository(AppDbContext context, ILoginRepository loginRepository, IOTPGenerateRepository oTPGenerateRepository,
            LoginBusiness loginBusiness)
        {
            _context = context;
            _loginRepository = loginRepository;
            _oTPGenerateRepository = oTPGenerateRepository;
            _loginBusiness = loginBusiness;
        }

        /// <summary>
        /// Método que busca todos os usuários cadastrados no sistema.
        /// </summary>  
        [HttpGet]
        public async Task<List<Usuario>> GetAllUsers()
        {
            try
            {
                return await _context.Usuarios.ToListAsync();
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar a lista de usuários. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método que busca um usuário pelo seu ID.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetUserByID(string id)
        {
            try
            {
                return await _context.Usuarios.SingleOrDefaultAsync(x => x.IDUSUARIO == id);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o id do usuário solicitado. Tente novamente mais tarde!");
            }

        }

        /// <summary>
        /// Método que busca um usuário pelo seu CPF.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetUserByCPF(string cpf)
        {
            try
            {
                return await _context.Usuarios.SingleOrDefaultAsync(x => x.CPFUSUARIO == cpf);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o CPF do usuário solicitado. Tente novamente mais tarde!");
            }

        }

        /// <summary>
        /// Método que busca um usuário pelo seu CPF.
        /// </summary>
        [HttpGet]
        public async Task<ActionResult<Usuario>> GetUserByEmail(string email)
        {
            try
            {
                return await _context.Usuarios.SingleOrDefaultAsync(x => x.DESCEMAIL == email);
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível carregar o Email do usuário solicitado. Tente novamente mais tarde!");
            }

        }

        /// <summary>
        /// Método para verificar se já existe um usuário no sistema com esse CPF.
        /// </summary>
        [HttpGet]
        public bool VerificaUsuarioExistente(string id)
        {
            try
            {
                if (_context.Usuarios.Any(x => x.IDUSUARIO == id))
                    return true; return false;
            }
            catch (Exception)
            {
                throw new Exception("Não foi possível verificar se o usuário já exite no sistema. Tente novamente mais tarde!");
            }
        }

        /// <summary>
        /// Método para cadastrar um novo usuário no sistema.
        /// </summary>
        [HttpPost]
        public async Task<ActionResult<Usuario>> CadastroUsuario(Usuario user, UsuarioSistema login, IList<int> listaGrupoAcesso)
        {
            try
            {
                if (!VerificaUsuarioExistente(user.IDUSUARIO))
                {
                    novoUsuario.IDUSUARIO = user.IDUSUARIO;
                    novoUsuario.IDUNIDADEINST = user.IDUNIDADEINST;
                    novoUsuario.IDINSTITUICAO = user.IDINSTITUICAO;
                    novoUsuario.IDINSTITUICAOUSUARIO = user.IDINSTITUICAOUSUARIO;
                    novoUsuario.DESCNOMEUSUARIO = user.DESCNOMEUSUARIO;
                    novoUsuario.DESCEMAIL = user.DESCEMAIL;
                    novoUsuario.CODCRIADOPOR = user.CODCRIADOPOR;
                    novoUsuario.DATAHORACRIACAO = DateTime.Now;

                    await _context.Usuarios.AddAsync(user);
                    await _context.SaveChangesAsync();

                    login.IDUSUARIO = user.IDUSUARIO;
                    login.LOGIN = user.IDUSUARIO;
                    login.SECRETKEY = "kagdbKUGASBUDHKBasdudbkHSos";
                    login.BOLPRIMEIROLOGIN = true;
                    login.CODCRIADOPOR = user.CODCRIADOPOR;
                    login.DATAHORACRIACAO = DateTime.Now;

                    await _context.Login.AddAsync(login);
                    await _context.SaveChangesAsync();


                    foreach (var item in listaGrupoAcesso)
                    {
                        var perfilUser = new UsuarioSistemaGrupoAcesso();
                        perfilUser.IDGRUPOACESSO = item;
                        perfilUser.IDUSUARIOSISTEMA = login.IDUSUARIOSISTEMA;
                        perfilUser.DATAHORACRIACAO = DateTime.Now;
                        await _context.UsuarioGrupoAcesso.AddAsync(perfilUser);
                    }
                    await _context.SaveChangesAsync();

                    var secretKey = _oTPGenerateRepository.GerarSecretKey(user.DESCEMAIL);
                    var qrCode = _oTPGenerateRepository.CriarQRCode(secretKey);

                    _loginBusiness.EnviarEmailPrimeiroAcesso(user.DESCNOMEUSUARIO, user.IDUSUARIO, user.DESCEMAIL, qrCode);
                }
                return novoUsuario;
            }
            catch (Exception)
            {
                _context.Usuarios.Where(a => a.IDUSUARIO == user.IDUSUARIO).ExecuteDelete();
                _context.Login.Where(a => a.IDUSUARIO == login.IDUSUARIO).ExecuteDelete();
                _context.UsuarioGrupoAcesso.Where(a => a.IDUSUARIOSISTEMA == login.IDUSUARIOSISTEMA).ExecuteDelete();
                await _context.SaveChangesAsync();
                throw new Exception("Não foi possível finalizar o cadastro do usuário. Tente novamente mais tarde!");
            }
}

/// <summary>
/// Método que atualiza o cadastro de um usuário.
/// </summary>
[HttpPatch]
public async Task<ActionResult<Usuario>> UpdateUsuario(Usuario user)
{
    try
    {
        var usuario = await _context.Usuarios.FindAsync(user.IDUSUARIO);
        if (usuario != null)
        {
            usuario.IDUSUARIO = user.IDUSUARIO;
            usuario.IDUNIDADEINST = user.IDUNIDADEINST;
            usuario.IDINSTITUICAO = user.IDINSTITUICAO;
            usuario.NUMCHECKALTERACAO = user.NUMCHECKALTERACAO;
            usuario.IDINSTITUICAOUSUARIO = user.IDINSTITUICAOUSUARIO;
            usuario.DESCNOMEUSUARIO = user.DESCNOMEUSUARIO;
            usuario.CPFUSUARIO = user.CPFUSUARIO;
            usuario.DATANASCIMENTOUSUARIO = user.DATANASCIMENTOUSUARIO;
            usuario.DESCEMAIL = user.DESCEMAIL;
            usuario.CELUSUARIO = user.CELUSUARIO;
            usuario.BOLHABILITADOUSUARIO = user.BOLHABILITADOUSUARIO;
            usuario.DESCSTATUSUSUARIO = user.DESCSTATUSUSUARIO;
            usuario.BOLVERIFICANOMEMAQUINA = user.BOLVERIFICANOMEMAQUINA;
            usuario.CODCRIADOPOR = user.CODCRIADOPOR;
            usuario.DATAHORACRIACAO = user.DATAHORACRIACAO;
            usuario.CODALTERADOPOR = user.CODALTERADOPOR;
            usuario.DATAHORAALTERACAO = user.DATAHORAALTERACAO;
            usuario.CODINATIVOPOR = user.CODINATIVOPOR;
            usuario.DATAHORAINATIVO = user.DATAHORAINATIVO;
        }

        await _context.SaveChangesAsync();

        return user;
    }
    catch (Exception)
    {
        throw new Exception("Não foi possível atualizar o cadastro do usuário. Tente novamente mais tarde!");
    }
}
    }
}
