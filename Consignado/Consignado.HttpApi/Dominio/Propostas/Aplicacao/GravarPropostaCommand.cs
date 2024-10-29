using CSharpFunctionalExtensions;
using System.ComponentModel.DataAnnotations;
using static Consignado.Controllers.PropostaController;

namespace Consignado.HttpApi.Dominio.Propostas.Aplicacao
{
    public class GravarPropostaCommand
    {
        public string CpfAgente { get; set; }

        public string Cpf { get; set; }
        public DateTime DataNascimento { get; set; }
        public string DDD { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public string Cep { get; set; }

        public string Endereco { get; set; }

        public string Numero { get; set; }

        public string Cidade { get; set; }

        public string Uf { get; set; }

        public string CodigoConveniada { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public string Prazo { get; set; }
        public decimal ValorOperacao { get; set; }
        public decimal Prestacao { get; set; }

        public string Matricula { get; set; }

        public decimal ValorRendimento { get; set; }

        public string Banco { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public Tipoconta TipoConta { get; set; }

        public GravarPropostaCommand(string cpfAgente, string cpf, string ddd, DateTime dataNascimento, string telefone,
                                 string email, string cep, string endereco, string numero, string cidade,
                                 string uf, string codigoConveniada, TipoOperacao tipoOperacao, string prazo,
                                 decimal valorOperacao, decimal prestacao, string matricula,
                                 decimal valorRendimento, string banco, string agencia, string conta,
                                 Tipoconta tipoConta)
        {
            CpfAgente = cpfAgente;
            Cpf = cpf;
            DDD = ddd;
            DataNascimento = dataNascimento;
            Telefone = telefone;
            Email = email;
            Cep = cep;
            Endereco = endereco;
            Numero = numero;
            Cidade = cidade;
            Uf = uf;
            CodigoConveniada = codigoConveniada;
            TipoOperacao = tipoOperacao;
            Prazo = prazo;
            ValorOperacao = valorOperacao;
            Prestacao = prestacao;
            Matricula = matricula;
            ValorRendimento = valorRendimento;
            Banco = banco;
            Agencia = agencia;
            Conta = conta;
            TipoConta = tipoConta;
        }
    }
}
