using Microsoft.AspNetCore.Mvc;
using Sicoob.API.AuthOriginal.Model;

namespace Sicoob.API.AuthOriginal.Repository.Interface
{
    public interface ILoginRepository
    {
        Task<List<UsuarioSistema>> GetAllUsersLogin();
        Task<UsuarioSistema> GetAllUsersLoginByID(string id);
        Task<UsuarioSistema> GetAllUsersLoginByLogin(string login);
        bool VerificaLoginExistente(string login);
        bool PrimeiroAcessoMail(string email);
    }
}
