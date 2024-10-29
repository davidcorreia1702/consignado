using Consignado.HttpApi.Dominio.Propostas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Consignado.HttpApi.Dominio.Propostas.Infraestrutura.Mapeamento
{
    public class DddMap : IEntityTypeConfiguration<DDD>
    {
        public void Configure(EntityTypeBuilder<DDD> builder)
        {
            throw new NotImplementedException();
        }
    }
}
