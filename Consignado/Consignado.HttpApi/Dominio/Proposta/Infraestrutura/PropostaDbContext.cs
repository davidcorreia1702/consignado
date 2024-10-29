using Microsoft.EntityFrameworkCore;

namespace Consignado.HttpApi.Dominio.Inscricao.Infraestrutura
{
    public class PropostaDbContext : DbContext
    {
        public PropostaDbContext(DbContextOptions<PropostaDbContext> options) : base(options)
        {

        }

        public DbSet<Proposta> Propostas { get; set; }
        public DbSet<Conveniada> Conveniadas { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
    }
}
