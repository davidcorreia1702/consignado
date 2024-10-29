using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada
{
    public interface IValidarProposta
    {
        Result Validar(ValidacaoProposta validacaoProposta);
    }
}
