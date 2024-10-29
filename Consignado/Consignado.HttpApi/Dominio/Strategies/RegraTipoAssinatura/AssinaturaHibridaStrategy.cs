using Consignado.HttpApi.Dominio.Propostas;

namespace Consignado.HttpApi.Dominio.Strategies.RegraTipoAssinatura
{
    public class AssinaturaHibridaStrategy : ITipoAssinaturaStrategy
    {
        public TipoAssinatura ObterTipoAssinatura()
        {
            return TipoAssinatura.Hibrida;
        }

        public bool Validar(ValidacaoTipoAssinatura validacaoTipoAssinatura)
        {
            return validacaoTipoAssinatura.AssinaturaHibirda;
        }
    }
}
