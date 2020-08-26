using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnboardSIGDB1Dominio.Cargos.Entidades;

namespace OnboardSIGDB1.Infra.Data.Cargos.Mappings
{
    public class CargoMapping : IEntityTypeConfiguration<Cargo>
    {
        public void Configure(EntityTypeBuilder<Cargo> builder)
        {
            builder.Property(x => x.Descricao)
                .HasMaxLength(250)
                .IsRequired();
            builder.HasIndex(x => x.Descricao).IsUnique();

            builder.Ignore(x => x.CascadeMode);

            builder.ToTable("Cargos");
        }
    }
}
