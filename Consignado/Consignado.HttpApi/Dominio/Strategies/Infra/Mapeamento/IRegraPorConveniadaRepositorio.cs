using Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada;

namespace Consignado.HttpApi.Dominio.Strategies.Infra.Mapeamento
{
    public interface IRegraPorConveniadaRepositorio
    {
        Task<IEnumerable<RegraPorConveniada>> ObterRegrasPorConveniadaAsync(int conveniadaId);
    }
}
