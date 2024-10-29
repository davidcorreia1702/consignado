using Consignado.Api.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.Api.SeedWork.EFCore.Mappings
{
    public class UnidadeFederativaMap : IEntityTypeConfiguration<UnidadeFederativa>
    {
        public void Configure(EntityTypeBuilder<UnidadeFederativa> builder)
        {
            builder.ToTable("UnidadeFederativa");

            builder.HasKey(c => c.Sigla);

            builder
           !.HasMany(p => p.Ddds)
           !.WithOne()
           !.OnDelete(DeleteBehavior.Cascade)
           !.IsRequired()
           !.Metadata
           !.PrincipalToDependent
           !.SetField("_ddds");
        }
    }
}
