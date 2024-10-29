using Consignado.HttpApi.Dominio.Propostas;

namespace Consignado.HttpApi.Dominio.Strategies.RegraTipoAssinatura
{
    public class AssinaturaEletronicaStrategy : ITipoAssinaturaStrategy
    {
        public TipoAssinatura ObterTipoAssinatura()
        {
            return TipoAssinatura.Eletronica;
        }

        public bool Validar(ValidacaoTipoAssinatura validacaoTipoAssinatura)
        {
            return validacaoTipoAssinatura.UfDdd == validacaoTipoAssinatura.UfNascimento;
        }
    }
}
