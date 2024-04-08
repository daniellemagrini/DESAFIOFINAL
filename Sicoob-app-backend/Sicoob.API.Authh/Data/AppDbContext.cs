using Microsoft.EntityFrameworkCore;
using Sicoob.API.Authh.Model;

namespace Sicoob.API.Authh.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Perfil> Perfils { get; set; }
        public DbSet<UsuarioCadastro> UsuariosCadastro { get; set; }
    }
}
