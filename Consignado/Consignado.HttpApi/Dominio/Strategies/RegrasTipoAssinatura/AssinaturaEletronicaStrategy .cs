using Consignado.HttpApi.Dominio.Propostas;
using Consignado.HttpApi.Dominio.Strategies.RegrasTipoAssinatura;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasTipoAssinatura
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
