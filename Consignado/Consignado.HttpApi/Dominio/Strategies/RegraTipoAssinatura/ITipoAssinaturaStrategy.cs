using Consignado.HttpApi.Dominio.Propostas;

namespace Consignado.HttpApi.Dominio.Strategies.RegraTipoAssinatura
{
    public interface ITipoAssinaturaStrategy
    {
        TipoAssinatura ObterTipoAssinatura();
        bool Validar(ValidacaoTipoAssinatura validacaoTipoAssinatura);
    }
}
