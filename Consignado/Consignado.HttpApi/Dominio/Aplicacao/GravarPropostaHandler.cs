using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Entidade;
using Consignado.HttpApi.Dominio.Factories;
using Consignado.HttpApi.Dominio.Infraestrutura;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

namespace Consignado.HttpApi.Dominio.Aplicacao
{
    public class GravarPropostaHandler
    {
        private readonly IPropostaRepositorio _propostaRepositorio;
        private readonly PropostaFactory _propostaFactory;

        public GravarPropostaHandler(
            IPropostaRepositorio propostaRepositorio,
            PropostaFactory propostaFactory)
        {
            _propostaRepositorio = propostaRepositorio;
            _propostaFactory = propostaFactory;
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

            var propostaResult = _propostaFactory.Gravar(command, conveniada.Value);
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
