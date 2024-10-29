using Consignado.HttpApi.Dominio.Regras.RegrasPorConveniada;

namespace Consignado.HttpApi.Dominio.Regras.Infra.Mapeamento
{
    public interface IRegraPorConveniadaRepositorio
    {
        Task<IEnumerable<RegraPorConveniada>> ObterRegrasPorConveniadaAsync(int conveniadaId);
    }
}
