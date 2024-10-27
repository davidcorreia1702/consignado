using Consignado.HttpApi.Dominio.Entidade;
using Consignado.HttpApi.Dominio.Infraestrutura;
using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Aplicacao
{
    public class GravarPropostaHandler
    {
        private readonly IPropostaRepositorio _propostaRepositorio;

        public GravarPropostaHandler(
            IPropostaRepositorio propostaRepositorio)
        {
            _propostaRepositorio = propostaRepositorio;
        }

        public async Task<Result<Proposta>> Handle(GravarPropostaCommand command, CancellationToken cancellationToken)
        {
            //consultar as propostas por cpf
            //verificar se exite proposta com situacao em aberto
            if (await _propostaRepositorio.ExistePropostaEmAberto(command.Cpf))
                return Result.Failure<Proposta>("Possui propostas em aberto");

            //consultar cpf bloqueados
            //verificar se cpf não está bloqueado
            if (await _propostaRepositorio.VerificarCpfBloqueado(command.Cpf))
                return Result.Failure<Proposta>("Cpf bloqueado");

            //consultar os agentes
            //verificar se o agente está ativo
            if (await _propostaRepositorio.VerificarAgenteInativo(command.Cpf))
                return Result.Failure<Proposta>("Agente inativo bloqueado");

            //consultar conveniadas 
            //verificar se a minha proposta é do tipo refinanciamento e se é aceita pelo convenio
            var conveniada = await _propostaRepositorio.RecuperarConveniada(command.CodigoConveniada, cancellationToken);
            if (conveniada.HasNoValue)
                return Result.Failure<Proposta>("Conveniada inválida");

            var propostaResult = Proposta.Criar(
                cpfAgente: command.CpfAgente,
                cpf: command.Cpf,
                dataNascimento: command.DataNascimento,
                ddd: command.DDD,
                telefone: command.Telefone,
                email: command.Email,
                cep: command.Cep,
                endereco: command.Endereco,
                numero: command.Numero,
                cidade: command.Cidade,
                uf: command.Uf,
                tipoOperacao: command.TipoOperacao,
                matricula: command.Matricula,
                valorRendimento: command.ValorRendimento,
                prazo: command.Prazo,
                valorOperacao: command.ValorOperacao,
                prestacao: command.Prestacao,
                banco: command.Banco,
                agencia: command.Agencia,
                conta: command.Conta,
                tipoConta: command.TipoConta,
                conveniada.Value);

            if(propostaResult.IsFailure)
                return Result.Failure<Proposta>(propostaResult.Error);

            //Salvar Proposta
            var proposta = propostaResult.Value;
            await _propostaRepositorio.Adicionar(proposta, cancellationToken);
            await _propostaRepositorio.Save();

            return Result.Success(proposta);
        }
    }
}
