using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada
{
    public class ValidacaoIdadeMaximaUltimaParcela : IValidarProposta
    {
        public Result Validar(ValidacaoProposta validacaoProposta)
        {
            if (validacaoProposta.Prazo == null || validacaoProposta.DataNascimento == null)
                return Result.Failure("Prazo ou Data de Nascimento não informados");

            var dataFutura = DateTime.Now.AddMonths(validacaoProposta.Prazo.Value);
            var diferencaEmAnos = dataFutura.Year - validacaoProposta.DataNascimento.Value.Year;

            if (dataFutura < validacaoProposta.DataNascimento.Value.AddYears(diferencaEmAnos))
                diferencaEmAnos--;

            if (diferencaEmAnos > 80)
                return Result.Failure("Idade ao realizar a ultima parcela excede de 80 anos");

            return Result.Success();
        }
    }
}
