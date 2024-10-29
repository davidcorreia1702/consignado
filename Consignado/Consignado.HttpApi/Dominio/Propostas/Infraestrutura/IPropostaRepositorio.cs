using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Propostas.Infraestrutura
{
    public interface IPropostaRepositorio
    {
        Task<bool> ExistePropostaEmAberto(string cpf);
        Task<bool> VerificarCpfBloqueado(string cpf);
        Task<bool> VerificarAgenteInativo(string cpfAgente);
        Task<Maybe<Conveniada>> RecuperarConveniada(string codigo, CancellationToken cancellationToken);
        Task<Maybe<Cliente>> RecuperarCliente(string cpf, CancellationToken cancellationToken);
        Task Adicionar(Proposta proposta, CancellationToken cancellationToken);
        Task Save();
    }
}
