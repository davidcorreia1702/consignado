using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada;
using Consignado.HttpApi.Dominio.Strategies.RegraTipoAssinatura;
using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Propostas
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
            int conveniadaId,
            TipoOperacao tipoOperacao,
            string matricula,
            decimal valorRendimento,
            string prazo,
            decimal valorOperacao,
            decimal prestacao,
            string banco,
            string agencia,
            string conta,
            Tipoconta tipoConta,
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
            ConveniadaId = conveniadaId;
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
        public int ConveniadaId { get; }
        public TipoOperacao TipoOperacao { get; }
        public string Matricula { get; }
        public decimal ValorRendimento { get; }
        public string Prazo { get; }
        public decimal ValorOperacao { get; }
        public decimal Prestacao { get; }
        public string Banco { get; }
        public string Agencia { get; }
        public string Conta { get; }
        public Tipoconta TipoConta { get; }
        public TipoAssinatura TipoAssinatura { get; }
        public SituacaoProposta Situacao { get;}
        public Conveniada Conveniada { get;}

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
            UnidadeFederativa uf,
            TipoOperacao tipoOperacao,
            string matricula,
            decimal valorRendimento,
            string prazo,
            decimal valorOperacao,
            decimal prestacao,
            string banco,
            string agencia,
            string conta,
            Tipoconta tipoConta,
            Conveniada conveniada,
            string ufDdd,
            IEnumerable<IValidarProposta> regras)
        {

            foreach (var regra in regras)
            {
                ValidacaoProposta validacaoProposta = new ValidacaoProposta { Conveniada = conveniada, TipoOperacao = tipoOperacao, Uf = uf.Sigla, ValorOperacao = valorOperacao, Prazo = Convert.ToInt32(prazo), DataNascimento = dataNascimento };  
                var resultado = regra.Validar(validacaoProposta);
                if (resultado.IsFailure)
                    return Result.Failure<Proposta>(resultado.Error);
            }

            var tipoAssinatura = ObterTipoAssinatura(ufDdd, uf);
            if (tipoAssinatura.IsFailure)
                return Result.Failure<Proposta>(tipoAssinatura.Error);

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
                uf.Sigla,
                conveniada.Id,
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
                tipoAssinatura: tipoAssinatura.Value,
                SituacaoProposta.EmAnalise
            );

            return Result.Success(proposta);
        }

        private static Result<TipoAssinatura> ObterTipoAssinatura(string ufDdd, UnidadeFederativa uf)
        {
            var validacoes = new List<ITipoAssinaturaStrategy>
            {
                new AssinaturaHibridaStrategy(),
                new AssinaturaEletronicaStrategy(),
                new AssinaturaFigitalStrategy()
            };

            foreach (var validacao in validacoes)
            {
                var resultado = validacao.Validar(new ValidacaoTipoAssinatura { UfDdd = ufDdd, UfNascimento = uf.Sigla, AssinaturaHibirda = uf.AssinaturaHibirda});
                if (resultado)
                    return Result.Success(validacao.ObterTipoAssinatura());
            }

            return Result.Failure<TipoAssinatura>("Tipo assinatura inválida");
        }
    }

    public enum TipoOperacao { Novo, Portabilidade, Refinanciamento }
    public enum TipoAssinatura { Eletronica, Hibrida, Figital }
    public enum SituacaoProposta { EmAnalise, Aprovada, Recusada }
    public enum Tipoconta { Poupanca, ContaCorrente, CartaoMagnetico }
}
