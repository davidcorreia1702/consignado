using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Consignado.HttpApi.Dominio.Propostas.Infraestrutura.Mapeamento
{
    public class AgenteMap : IEntityTypeConfiguration<Agente>
    {
        public void Configure(EntityTypeBuilder<Agente> builder)
        {
            builder.ToTable("Agente");

            builder.HasKey(a => a.Cpf);

            builder.Property(a => a.Ativo)
                .IsRequired();
        }
    }
}
