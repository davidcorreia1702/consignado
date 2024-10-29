using Consignado.Api.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.Api.SeedWork.EFCore.Mappings
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
