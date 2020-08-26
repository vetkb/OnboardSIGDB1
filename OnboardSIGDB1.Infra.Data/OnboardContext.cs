using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnboardSIGDB1.Infra.Data.Cargos.Mappings;
using OnboardSIGDB1.Infra.Data.Empresas.Mappings;
using OnboardSIGDB1.Infra.Data.Funcionarios.Mappings;
using OnboardSIGDB1Dominio.Cargos.Entidades;
using OnboardSIGDB1Dominio.Empresas.Entidades;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;

namespace OnboardSIGDB1.Infra.Data
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
            modelBuilder.ApplyConfiguration(new CargoMapping());
            modelBuilder.ApplyConfiguration(new EmpresaMapping());
            modelBuilder.ApplyConfiguration(new FuncionarioMapping());
        }

        public DbSet<Empresa> Empresas { get; set; }
        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<Cargo> Cargos { get; set; }
    }
}
