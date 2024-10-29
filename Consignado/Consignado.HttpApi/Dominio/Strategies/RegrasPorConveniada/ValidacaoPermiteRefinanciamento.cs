using Consignado.HttpApi.Dominio.Propostas;
using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada
{
    public class ValidacaoPermiteRefinanciamento : IValidarProposta
    {
        public Result Validar(ValidacaoProposta validacaoProposta)
        {
            if (validacaoProposta.TipoOperacao == TipoOperacao.Refinanciamento && !validacaoProposta.Conveniada.AceitaRefinanciamento)
                return Result.Failure("Conveniada não aceita Refinanciamento");

            return Result.Success();
        }
    }
}
