using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.HttpApi.Dominio.Strategies.Infra.Mapeamento
{
    public class RegraPorConveniadaMap : IEntityTypeConfiguration<RegraPorConveniada>
    {
        public void Configure(EntityTypeBuilder<RegraPorConveniada> builder)
        {
            builder.ToTable("RegrasPorConveniada");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.ConveiadaId);
            builder
                .Property(p => p.Regra)
                .HasColumnType("varchar(max)")
                .HasConversion(
                    c => c.ToNameTypeJson(),
                    s => s.ToNameTypeObject<IValidarProposta>())
                .IsRequired();
        }
    }
}
