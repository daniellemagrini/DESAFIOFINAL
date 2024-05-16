using AceleraPlenoProjetoFinal.Api.Models;
using Microsoft.EntityFrameworkCore;
using Sicoob.API.AuthOriginal.Model;

namespace Sicoob.API.AuthOriginal.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<UsuarioSistema> Login { get; set; }
        public DbSet<UsuarioSistemaGrupoAcesso> UsuarioGrupoAcesso { get; set; }
        public DbSet<GrupoAcesso> GrupoAcesso { get; set; }
    }
}
