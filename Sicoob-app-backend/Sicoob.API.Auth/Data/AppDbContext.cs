using Microsoft.EntityFrameworkCore;
using Sicoob.API.Auth.Models;

namespace Sicoob.API.Auth.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<UsuarioModel> TBLOGIN { get; set; }
    }
}
