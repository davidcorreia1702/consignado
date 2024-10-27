using Consignado.HttpApi.Comum;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

namespace Consignado.HttpApi.Dominio.Entidade
{
    public class Proposta
    {
        private Proposta(
            Guid numeroProposta,
            string cpfAgente,
            string cpf,
            DateTime dataNascimento,
            string ddd,
            string telefone,
            string email,
            string cep,
            string endereco,
            string numero,
            string cidade,
            string uf,
            string codigoConveniada,
            TipoOperacao tipoOperacao,
            string matricula,
            decimal valorRendimento,
            string prazo,
            decimal valorOperacao,
            decimal prestacao,
            string banco,
            string agencia,
            string conta,
            string tipoConta,
            TipoAssinatura tipoAssinatura,
            SituacaoProposta situacao)
        {
            NumeroProposta = numeroProposta;
            CpfAgente = cpfAgente;
            Cpf = cpf;
            DataNascimento = dataNascimento;
            DDD = ddd;
            Telefone = telefone;
            Email = email;
            Cep = cep;
            Endereco = endereco;
            Numero = numero;
            Cidade = cidade;
            Uf = uf;
            CodigoConveniada = codigoConveniada;
            TipoOperacao = tipoOperacao;
            Matricula = matricula;
            ValorRendimento = valorRendimento;
            Prazo = prazo;
            ValorOperacao = valorOperacao;
            Prestacao = prestacao;
            Banco = banco;
            Agencia = agencia;
            Conta = conta;
            TipoConta = tipoConta;
            TipoAssinatura = tipoAssinatura;
            Situacao = situacao; 
        }

        public Guid NumeroProposta { get; }
        public string CpfAgente { get; }
        public string Cpf { get; }
        public DateTime DataNascimento { get; }
        public string DDD { get; }
        public string Telefone { get; }
        public string Email { get; }
        public string Cep { get; }
        public string Endereco { get; }
        public string Numero { get; }
        public string Cidade { get; }
        public string Uf { get; }
        public string CodigoConveniada { get; }
        public TipoOperacao TipoOperacao { get; }
        public string Matricula { get; }
        public decimal ValorRendimento { get; }
        public string Prazo { get; }
        public decimal ValorOperacao { get; }
        public decimal Prestacao { get; }
        public string Banco { get; }
        public string Agencia { get; }
        public string Conta { get; }
        public string TipoConta { get; }
        public TipoAssinatura TipoAssinatura { get; }
        public SituacaoProposta Situacao { get; set; }

        public static Result<Proposta> Criar(
            string cpfAgente,
            string cpf,
            DateTime dataNascimento,
            string ddd,
            string telefone,
            string email,
            string cep,
            string endereco,
            string numero,
            string cidade,
            string uf,
            TipoOperacao tipoOperacao,
            string matricula,
            decimal valorRendimento,
            string prazo,
            decimal valorOperacao,
            decimal prestacao,
            string banco,
            string agencia,
            string conta,
            string tipoConta,
            Conveniada conveniada)
        {

            if (tipoOperacao == TipoOperacao.Refinanciamento && !conveniada.AceitaRefinanciamento)
                return Result.Failure<Proposta>("Conveniada não aceita Refinanciamento");

            //validar restriçao de valores de acordo com o convenio
            if (!conveniada.Restricoes.IsNullOrEmpty() && conveniada.Restricoes.Any(restricao => restricao.Uf == uf && valorOperacao > restricao.ValorLimite))
                return Result.Failure<Proposta>("Conveniada com restricao para o valor solicitado");

            //somar a quantidade de parcelas a idade <= 80 anos
            var dataFutura = DateTime.Now.AddMonths(Convert.ToInt32(prazo));
            var diferencaEmAnos = dataFutura.Year - dataNascimento.Year;

            if (dataFutura < dataNascimento.AddYears(diferencaEmAnos))
                diferencaEmAnos--;

            if (diferencaEmAnos > 80)
                return Result.Failure<Proposta>("Idade ao realizar a ultima parcela excede de 80 anos");

            var dddCorrespondeUf = DddUfMapping.DddToUf.TryGetValue(ddd, out var ufDoDdd) && ufDoDdd == uf;

            var tipoAssinatura = (ddd, uf) switch
            {
                _ when UfComAssinaturaHibrida.Ufs.Contains(uf) => TipoAssinatura.Hibrida,  // Assinatura Híbrida se a UF estiver na lista
                _ when dddCorrespondeUf => TipoAssinatura.Eletronica,       // Assinatura Eletrônica se o DDD for igual à UF de Residência
                _ => TipoAssinatura.Figital                                 // Assinatura Figital se DDD for diferente da UF de Residência
            };

            var proposta = new Proposta(
                Guid.NewGuid(),
                cpfAgente,
                cpf,
                dataNascimento,
                ddd,
                telefone,
                email,
                cep,
                endereco,
                numero,
                cidade,
                uf,
                conveniada.Codigo,
                tipoOperacao,
                matricula,
                valorRendimento,
                prazo,
                valorOperacao,
                prestacao,
                banco,
                agencia,
                conta,
                tipoConta,
                tipoAssinatura,
                SituacaoProposta.EmAnalise
            );

            return Result.Success(proposta);
        }
    }

    public enum TipoOperacao { Novo, Portabilidade, Refinanciamento }
    public enum TipoAssinatura { Eletronica, Hibrida, Figital }
    public enum SituacaoProposta { EmAnalise, Aprovada, Recusada }
}
