using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnboardSIGDB1.DAL.Models;

namespace OnboardSIGDB1.DAL
{
    public class OnboardContext : DbContext
    {
        public IConfiguration _configuration;

        public OnboardContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_configuration.GetConnectionString("DefaultConnection"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empresa>().HasIndex(x => x.Cnpj).IsUnique();
            modelBuilder.Entity<Funcionario>().HasIndex(x => x.Cpf).IsUnique();
            modelBuilder.Entity<Cargo>().HasIndex(x => x.Descricao).IsUnique();
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
    }
}
