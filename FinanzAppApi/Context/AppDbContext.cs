using FinanzAppApi.Models;
using Microsoft.EntityFrameworkCore;

namespace FinanzAppApi.Context
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Ingreso> Ingresos { get; set; }
        public DbSet<Gasto> Gastos { get; set; }
        public DbSet<Cuenta> Cuentas { get; set; }
        public DbSet<Usuario> Usuarios { get; set; }
    }
}
