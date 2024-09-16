using Microsoft.EntityFrameworkCore;
using Sistema_Pessoal_de_Gerenciamento_de_Despesas.Models;

namespace Sistema_Pessoal_de_Gerenciamento_de_Despesas.Date
{
    public class BancoContexto : DbContext
    {
        public BancoContexto(DbContextOptions<BancoContexto> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new Usuarios());
        }

        public DbSet<Models.Usuarios> Login { get; set; }
    }
}
