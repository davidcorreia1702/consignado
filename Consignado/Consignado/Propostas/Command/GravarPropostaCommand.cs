using Consignado.Api.Propostas.Enum;
using System.ComponentModel.DataAnnotations;

namespace Consignado.Api.Propostas.Command
{
    public class GravarPropostaCommand
    {
        public string CpfAgente { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório")]
        public string Cpf { get; set; }
        [Required(ErrorMessage = "A Data de nascimento é obrigatório")]
        public DateTime DataNascimento { get; set; }
        [Required(ErrorMessage = "O DDD é obrigatório")]
        public string DDD { get; set; }

        [Required(ErrorMessage = "O telefone é obrigatório")]
        public string Telefone { get; set; }

        [Required(ErrorMessage = "O email é obrigatório")]
        public string Email { get; set; }

        public string Cep { get; set; }

        [Required(ErrorMessage = "O endereço  é obrigatório")]
        public string Endereco { get; set; }

        [Required(ErrorMessage = "O número  é obrigatório")]
        public string Numero { get; set; }

        [Required(ErrorMessage = "A cidade  é obrigatória")]
        public string Cidade { get; set; }

        [Required(ErrorMessage = "A UF  é obrigatória")]
        public string Uf { get; set; }

        public string CodigoConveniada { get; set; }
        public TipoOperacao TipoOperacao { get; set; }
        public string Prazo { get; set; }
        public decimal ValorOperacao { get; set; }
        public decimal Prestacao { get; set; }

        [Required(ErrorMessage = "A matrícula é obrigatória")]
        public string Matricula { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "O valor do rendimento deve ser maior que zero")]
        public decimal ValorRendimento { get; set; }

        [Required(ErrorMessage = "O banco é obrigatório")]
        public string Banco { get; set; }

        [Required(ErrorMessage = "A agência é obrigatória")]
        public string Agencia { get; set; }

        [Required(ErrorMessage = "A conta é obrigatória")]
        public string Conta { get; set; }

        [Required(ErrorMessage = "O tipo de conta é obrigatório")]
        public Tipoconta TipoConta { get; set; }

        public GravarPropostaCommand(
                string cpfAgente, 
                string cpf, 
                string ddd, 
                DateTime dataNascimento, 
                string telefone, 
                string email, 
                string cep, 
                string endereco, 
                string numero, 
                string cidade, 
                string uf, string codigoConveniada, 
                TipoOperacao tipoOperacao, 
                string prazo, 
                decimal valorOperacao, 
                decimal prestacao, 
                string matricula,
                decimal valorRendimento, 
                string banco, 
                string agencia, 
                string conta, 
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
