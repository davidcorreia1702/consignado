using System.ComponentModel.DataAnnotations;
using static Consignado.Controllers.PropostaController;

namespace Consignado.HttpApi.Dominio.Propostas.Aplicacao
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

        public GravarPropostaCommand(MovaPropostaModel model)
        {
            CpfAgente = model.CpfAgente;
            Cpf = model.Cpf;
            DDD = model.DDD;
            DataNascimento = model.DataNascimento;
            Telefone = model.Telefone;
            Email = model.Email;
            Cep = model.Cep;
            Endereco = model.Endereco;
            Numero = model.Numero;
            Cidade = model.Cidade;
            Uf = model.Uf;
            CodigoConveniada = model.CodigoConveniada;
            TipoOperacao = model.TipoOperacao;
            Prazo = model.Prazo;
            ValorOperacao = model.ValorOperacao;
            Prestacao = model.Prestacao;
            Matricula = model.Matricula;
            ValorRendimento = model.ValorRendimento;
            Banco = model.Banco;
            Agencia = model.Agencia;
            Conta = model.Conta;
            TipoConta = model.TipoConta;
        }
    }
}
