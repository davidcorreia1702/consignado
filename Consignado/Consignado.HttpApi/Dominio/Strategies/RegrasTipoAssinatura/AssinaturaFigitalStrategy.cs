using Consignado.HttpApi.Dominio.Propostas;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasTipoAssinatura
{
    public class AssinaturaFigitalStrategy : ITipoAssinaturaStrategy
    {
        public TipoAssinatura ObterTipoAssinatura()
        {
            return TipoAssinatura.Figital;
        }

        public bool Validar(ValidacaoTipoAssinatura validacaoTipoAssinatura)
        {
            return validacaoTipoAssinatura.UfDdd != validacaoTipoAssinatura.UfNascimento;
        }
    }
}
