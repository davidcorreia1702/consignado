using Consignado.HttpApi.Dominio.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.HttpApi.Dominio.Propostas.Infraestrutura.Mapeamento
{
    public class UnidadeFederativaMap : IEntityTypeConfiguration<UnidadeFederativa>
    {
        public void Configure(EntityTypeBuilder<UnidadeFederativa> builder)
        {
            throw new NotImplementedException();
        }
    }
}
