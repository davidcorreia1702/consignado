using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Aplicacao;
using Consignado.HttpApi.Dominio.Entidade;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;
using System.Threading;

namespace Consignado.HttpApi.Dominio.Factories
{
    public class PropostaFactory
    {
        List<string> ufs = new List<string> { "SP", "MG", "ES", "DF", "BA", "RS", "PR" };

        public Result<Proposta> Gravar(GravarPropostaCommand command, Conveniada conveniada)
        {
            if (command.TipoOperacao == TipoOperacao.Refinanciamento && !conveniada.AceitaRefinanciamento)
                return Result.Failure<Proposta>("Conveniada não aceita Refinanciamento");

            //validar restriçao de valores de acordo com o convenio
            if (!conveniada.Restricoes.IsNullOrEmpty() && conveniada.Restricoes.Any(restricao => restricao.Uf == command.Uf && command.ValorOperacao > restricao.ValorLimite))
                return Result.Failure<Proposta>("Conveniada com restricao para o valor solicitado");

            //somar a quantidade de parcelas a idade <= 80 anos
            var dataFutura = DateTime.Now.AddMonths(Convert.ToInt32(command.Prazo));
            var diferencaEmAnos = dataFutura.Year - command.DataNascimento.Year;

            if (dataFutura < command.DataNascimento.AddYears(diferencaEmAnos))
                diferencaEmAnos--;

            if (diferencaEmAnos > 80)
                return Result.Failure<Proposta>("Idade ao realizar a ultima parcela excede de 80 anos");

            var dddCorrespondeUf = DddUfMapping.DddToUf.TryGetValue(command.DDD, out var ufDoDdd) && ufDoDdd == command.Uf;

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

            proposta.TipoAssinatura = (command.DDD, command.Uf) switch
            {
                _ when ufs.Contains(command.Uf) => TipoAssinatura.Hibrida,  // Assinatura Híbrida se a UF estiver na lista
                _ when dddCorrespondeUf => TipoAssinatura.Eletronica,       // Assinatura Eletrônica se o DDD for igual à UF de Residência
                _ => TipoAssinatura.Figital                                 // Assinatura Figital se DDD for diferente da UF de Residência
            };

            return Result.Success(proposta);
        }
    }
}
