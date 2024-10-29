using Consignado.HttpApi.Dominio.Propostas.Infraestrutura;
using Consignado.HttpApi.Dominio.Strategies.Infra.Mapeamento;
using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Propostas.Aplicacao
{
    public class GravarPropostaHandler
    {
        private readonly IPropostaRepositorio _propostaRepositorio;
        private readonly IRegraPorConveniadaRepositorio _regraPorConveniadaRepositorio;

        public GravarPropostaHandler(
            IPropostaRepositorio propostaRepositorio,
            IRegraPorConveniadaRepositorio regraPorConveniadaRepositorio)
        {
            _propostaRepositorio = propostaRepositorio;
            _regraPorConveniadaRepositorio = regraPorConveniadaRepositorio;
        }

        public async Task<Result<Proposta>> Handle(GravarPropostaCommand command, CancellationToken cancellationToken)
        {
            var _validador = new GravarPropostaValidador();
            var resultadoValidacao = _validador.Validar(command);
            if (resultadoValidacao.IsFailure)
                return Result.Failure<Proposta>(resultadoValidacao.Error);

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

            //consultas as uf
            var ufDdd = await _propostaRepositorio.RecuperarUFPorDDD(command.DDD, cancellationToken);
            if (ufDdd.HasNoValue)
                return Result.Failure<Proposta>("DDD não possui uf assossiada");

            //consultas as uf
            var unidadeFederativa = await _propostaRepositorio.RecuperarUFPorDDD(command.DDD, cancellationToken);
            if (unidadeFederativa.HasNoValue)
                return Result.Failure<Proposta>("UF inválida");

            var regras = await _regraPorConveniadaRepositorio.ObterRegrasPorConveniadaAsync(conveniada.Value.Id);

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
                uf: unidadeFederativa.Value,
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
                conveniada.Value,
                ufDdd: ufDdd.Value.Sigla,
                regras.Select(n => n.Regra));

            if (propostaResult.IsFailure)
                return Result.Failure<Proposta>(propostaResult.Error);

            //Salvar Proposta
            var proposta = propostaResult.Value;
            await _propostaRepositorio.Adicionar(proposta, cancellationToken);
            await _propostaRepositorio.Save();

            return Result.Success(proposta);
        }
    }
}
