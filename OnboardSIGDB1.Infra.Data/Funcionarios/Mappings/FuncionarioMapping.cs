using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1Dominio.Funcionarios.Entidades;

namespace OnboardSIGDB1.Infra.Data.Funcionarios.Mappings
{
    public class FuncionarioMapping : IEntityTypeConfiguration<Funcionario>
    {
        public void Configure(EntityTypeBuilder<Funcionario> builder)
        {
            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Cpf)
                .HasMaxLength(11)
                .IsRequired();
            builder.HasIndex(x => x.Cpf).IsUnique();

            builder.Property(x => x.DataContratacao)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.Property(x => x.EmpresaId);
            builder.HasOne(x => x.Empresa)
                .WithMany(x => x.Funcionarios)
                .HasForeignKey(x => x.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(x => x.CargoId);
            builder.HasOne(x => x.Cargo)
                .WithMany(x => x.Funcionarios)
                .HasForeignKey(x => x.CargoId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Ignore(x => x.CascadeMode);

            builder.ToTable("Funcionarios");
        }
    }
}
