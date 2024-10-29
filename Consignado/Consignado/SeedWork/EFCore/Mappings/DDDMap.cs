using Consignado.Api.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.Api.SeedWork.EFCore.Mappings
{
    public class DDDMap : IEntityTypeConfiguration<DDD>
    {
        public void Configure(EntityTypeBuilder<DDD> builder)
        {
            builder.ToTable("DDD");


            builder.Property(a => a.Numero)
                .HasColumnType("varchar(2)")
                .IsRequired();
        }
    }
}
