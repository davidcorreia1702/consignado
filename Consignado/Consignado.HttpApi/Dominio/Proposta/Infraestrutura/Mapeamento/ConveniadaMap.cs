using CSharpFunctionalExtensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.HttpApi.Dominio.Inscricao.Infraestrutura.Mapeamento
{
    public class ConveniadaMap : IEntityTypeConfiguration<Conveniada>
    {
        public void Configure(EntityTypeBuilder<Conveniada> builder)
        {
            builder.ToTable("Conveniada");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Codigo)
                .IsRequired()
                .HasColumnType("varchar(6)");

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasColumnType("varchar(100)");

            builder.Property(a => a.AceitaRefinanciamento)
            .IsRequired();

            builder.HasMany(c => c.Restricoes)
                  .WithOne() 
                  .HasForeignKey("Id")
                  .IsRequired();
        }
    }
}
