using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Entidade;
using Consignado.HttpApi.Dominio.Infraestrutura;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

namespace Consignado.HttpApi.Dominio.Aplicacao
{
    public class GravarPropostaHandler
    {
        private readonly IPropostaRepositorio _propostaRepositorio;
        List<string> ufs = new List<string> { "SP", "RJ", "MG", "ES", "DF", "BA", "RS", "PR" };

        public GravarPropostaHandler(IPropostaRepositorio propostaRepositorio)
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

            //Criar Proposta
            var proposta = new Proposta();
            proposta.Cpf = command.Cpf;
            proposta.CpfAgente = command.CpfAgente;
            proposta.DataNascimento = command.DataNascimento;
            proposta.DDD = command.DDD;
            proposta.Telefone = command.Telefone;
            proposta.Email = command.Email;
            proposta.Endereco = command.Endereco;
            proposta.Numero = command.Numero;
            proposta.Cidade = command.Cidade;
            proposta.Cep = command.Cep;
            proposta.Uf = command.Uf;
            proposta.CodigoConveniada = command.CodigoConveniada;
            proposta.TipoOperacao = command.TipoOperacao;
            proposta.Matricula = command.Matricula;
            proposta.ValorRendimento = command.ValorRendimento;
            proposta.Prazo = command.Prazo;
            proposta.ValorOperacao = command.ValorOperacao;
            proposta.Prestacao = command.Prestacao;
            proposta.Banco = command.Banco;
            proposta.Agencia = command.Agencia;
            proposta.Conta = command.Conta;
            proposta.TipoConta = command.TipoConta;

            if (command.TipoOperacao == TipoOperacao.Refinanciamento && !conveniada.Value.AceitaRefinanciamento)
                return Result.Failure<Proposta>("Conveniada não aceita Refinanciamento");

            //validar restriçao de valores de acordo com o convenio
            if (!conveniada.Value.Restricoes.IsNullOrEmpty() && conveniada.Value.Restricoes.Any(restricao => restricao.Uf == command.Uf && command.ValorOperacao > restricao.ValorLimite))
                return Result.Failure<Proposta>("Conveniada com restricao para o valor solicitado");

            //somar a quantidade de parcelas a idade <= 80 anos
            var dataFutura = DateTime.Now.AddMonths(Convert.ToInt32(command.Prazo));
            var diferencaEmAnos = dataFutura.Year - command.DataNascimento.Year;

            if (dataFutura < command.DataNascimento.AddYears(diferencaEmAnos))
                diferencaEmAnos--;

            if (diferencaEmAnos > 80)
                return Result.Failure<Proposta>("Idade ao realizar a ultima parcela excede de 80 anos");

            var dddCorrespondeUf = DddUfMapping.DddToUf.TryGetValue(command.DDD, out var ufDoDdd) && ufDoDdd == command.Uf;

            proposta.TipoAssinatura = (command.DDD, command.Uf) switch
            {
                _ when ufs.Contains(command.Uf) => TipoAssinatura.Hibrida,  // Assinatura Híbrida se a UF estiver na lista
                _ when dddCorrespondeUf => TipoAssinatura.Eletronica,       // Assinatura Eletrônica se o DDD for igual à UF de Residência
                _ => TipoAssinatura.Figital                                 // Assinatura Figital se DDD for diferente da UF de Residência
            };

            //Salvar Proposta
            await _propostaRepositorio.Adicionar(proposta, cancellationToken);
            await _propostaRepositorio.Save();

            return Result.Success(proposta);
        }
    }
}
