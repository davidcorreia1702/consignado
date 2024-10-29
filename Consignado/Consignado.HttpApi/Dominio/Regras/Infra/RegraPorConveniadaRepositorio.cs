using Consignado.HttpApi.Dominio.Regras.Infra.Mapeamento;
using Consignado.HttpApi.Dominio.Regras.RegrasPorConveniada;
using Microsoft.EntityFrameworkCore;

namespace Consignado.HttpApi.Dominio.Regras.Infra
{
    public class RegraPorConveniadaRepositorio : IRegraPorConveniadaRepositorio
    {
        private readonly PropostaDbContext context;

        public RegraPorConveniadaRepositorio(PropostaDbContext context)
        {
            this.context = context;
        }

        public async Task<IEnumerable<RegraPorConveniada>> ObterRegrasPorConveniadaAsync(int conveniadaId)
        {
            return await context.RegrasPorConveniada
                    .Where(r => r.ConveiadaId == conveniadaId)
                    .ToListAsync();
        }
    }
}
