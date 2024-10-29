using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Regras.RegrasPorConveniada
{
    public interface IValidarProposta
    {
        Result Validar(ValidacaoProposta validacaoProposta);
    }
}
