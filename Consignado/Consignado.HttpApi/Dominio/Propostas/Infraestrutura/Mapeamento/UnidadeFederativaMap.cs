using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace Consignado.HttpApi.Dominio.Propostas.Infraestrutura.Mapeamento
{
    public class UnidadeFederativaMap : IEntityTypeConfiguration<UnidadeFederativa>
    {
        public void Configure(EntityTypeBuilder<UnidadeFederativa> builder)
        {
            builder.ToTable("UnidadeFederativas");

            builder.HasKey(c => c.Sigla);

            builder.Property(c => c.Nome)
            .IsRequired()
            .HasColumnType("varchar(100)");

            builder.Property(a => a.AssinaturaHibirda)
            .IsRequired();

            builder
                .HasMany(uf => uf.Ddds)
                .WithOne()
                .HasForeignKey(d => d.UnidadeFederativaId)
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
        }
    }
}
