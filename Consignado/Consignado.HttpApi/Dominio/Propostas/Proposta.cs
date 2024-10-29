using Consignado.HttpApi.Comum;
using Consignado.HttpApi.Dominio.Regras.RegrasPorConveniada;
using CSharpFunctionalExtensions;
using Microsoft.IdentityModel.Tokens;

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
            Tipoconta tipoConta,
            Conveniada conveniada,
            IEnumerable<IValidarProposta> regras)
        {

            foreach (var regra in regras)
            {
                ValidacaoProposta validacaoProposta = new ValidacaoProposta { Conveniada = conveniada, TipoOperacao = tipoOperacao, Uf = uf, ValorOperacao = valorOperacao, Prazo = Convert.ToInt32(prazo), DataNascimento = dataNascimento };  
                var resultado = regra.Validar(validacaoProposta);
                if (resultado.IsFailure)
                    return Result.Failure<Proposta>(resultado.Error);
            }

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
                tipoAssinatura: ObterTipoAssinatura(ddd, uf),
                SituacaoProposta.EmAnalise
            );

            return Result.Success(proposta);
        }

        public static TipoAssinatura ObterTipoAssinatura(string ddd, string uf)
        {
            var dddCorrespondeUf = DddUfMapping.DddToUf.TryGetValue(ddd, out var ufDoDdd) && ufDoDdd == uf;

            return (ddd, uf) switch
            {
                _ when UfComAssinaturaHibrida.Ufs.Contains(uf) => TipoAssinatura.Hibrida,  // Assinatura Híbrida se a UF estiver na lista
                _ when dddCorrespondeUf => TipoAssinatura.Eletronica,                      // Assinatura Eletrônica se o DDD for igual à UF de Residência
                _ => TipoAssinatura.Figital                                                 // Assinatura Figital se DDD for diferente da UF de Residência
            };
        }
    }

    public enum TipoOperacao { Novo, Portabilidade, Refinanciamento }
    public enum TipoAssinatura { Eletronica, Hibrida, Figital }
    public enum SituacaoProposta { EmAnalise, Aprovada, Recusada }
    public enum Tipoconta { Poupanca, ContaCorrente, CartaoMagnetico }
}
