using Microsoft.EntityFrameworkCore;
using Formulario_Cadastro_Cliente.Models;
namespace Formulario_Cadastro_Cliente.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options) { }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Endereco>Enderecos { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Cliente>().ToTable("clientes")
                .HasOne(c => c.Endereco)
                .WithOne(c => c.Cliente)
                .HasForeignKey<Endereco>(e => e.ClienteId);

            base.OnModelCreating(modelBuilder);
        }

    }
}
