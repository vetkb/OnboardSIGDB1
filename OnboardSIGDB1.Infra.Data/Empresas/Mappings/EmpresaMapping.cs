using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1Dominio.Empresas.Entidades;

namespace OnboardSIGDB1.Infra.Data.Empresas.Mappings
{
    public class EmpresaMapping : IEntityTypeConfiguration<Empresa>
    {
        public void Configure(EntityTypeBuilder<Empresa> builder)
        {
            builder.Property(x => x.Nome)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(x => x.Cnpj)
                .HasMaxLength(14)
                .IsRequired();
            builder.HasIndex(x => x.Cnpj).IsUnique();

            builder.Property(x => x.DataFundacao)
                .HasColumnType("datetime2")
                .IsRequired(false);

            builder.HasMany(x => x.Funcionarios)
                .WithOne(x => x.Empresa)
                .HasForeignKey(x => x.EmpresaId);

            builder.Ignore(x => x.CascadeMode);

            builder.ToTable("Empresas");
        }
    }
}
