using Consignado.Api.Propostas;
using Consignado.Api.SeedWork.EFCore.Mappings;
using Microsoft.EntityFrameworkCore;

namespace Consignado.Api.SeedWork.EFCore
{
    public class PropostaDbContext : DbContext
    {
        public PropostaDbContext(DbContextOptions<PropostaDbContext> options) : base(options)
        {

        }

        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Conveniada> Conveniadas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new AgenteMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ConveniadaMap());
            modelBuilder.ApplyConfiguration(new ConveniadaUfRestricaoMap());
            modelBuilder.ApplyConfiguration(new PropostaMap());
            modelBuilder.ApplyConfiguration(new DDDMap());
        }
    }
}
