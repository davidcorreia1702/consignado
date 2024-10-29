using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada
{
    public class ValidacaoRestricaoValor : IValidarProposta
    {
        public Result Validar(ValidacaoProposta validacaoProposta)
        {
            if (!validacaoProposta.Conveniada.Restricoes.IsNullOrEmpty() && validacaoProposta.Conveniada.Restricoes.Any(restricao => restricao.UF == validacaoProposta.Uf && validacaoProposta.ValorOperacao > restricao.ValorLimite))
                return Result.Failure("Conveniada com restricao para o valor solicitado");

            return Result.Success();
        }
    }
}
