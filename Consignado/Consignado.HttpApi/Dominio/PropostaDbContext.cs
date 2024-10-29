using Consignado.HttpApi.Dominio.Propostas;
using Consignado.HttpApi.Dominio.Propostas.Infraestrutura.Mapeamento;
using Consignado.HttpApi.Dominio.Regras.Infra.Mapeamento;
using Consignado.HttpApi.Dominio.Regras.RegrasPorConveniada;
using Microsoft.EntityFrameworkCore;

namespace Consignado.HttpApi.Dominio
{
    public class PropostaDbContext : DbContext
    {
        public PropostaDbContext(DbContextOptions<PropostaDbContext> options) : base(options){}

        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Conveniada> Conveniadas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Agente> Agentes { get; set; }
        public DbSet<ConveniadaUfRestricao> ConveniadaUfRestricoes { get; set; }
        public DbSet<UnidadeFederativa> UnidadesFederativa { get; set; }
        public DbSet<DDD> DDDs { get; set; }
        public DbSet<RegraPorConveniada> RegrasPorConveniada { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new PropostaMap());
            modelBuilder.ApplyConfiguration(new AgenteMap());
            modelBuilder.ApplyConfiguration(new ClienteMap());
            modelBuilder.ApplyConfiguration(new ConveniadaMap());
            modelBuilder.ApplyConfiguration(new ConveniadaUfRestricaoMap());
            modelBuilder.ApplyConfiguration(new UnidadeFederativaMap());
            modelBuilder.ApplyConfiguration(new DddMap());
            modelBuilder.ApplyConfiguration(new RegraPorConveniadaMap());
        }
    }
}
