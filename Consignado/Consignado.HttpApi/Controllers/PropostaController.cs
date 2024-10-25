using Consignado.HttpApi.Dominio.Entidade;
using Consignado.HttpApi.Dominio.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Linq;

namespace Consignado.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropostaController : ControllerBase
    {
        public record EnderecoModel(string CepResidencial, string EnderecoResidencial, int NumeroResidencial, string CidadeResidencial, string UfResidencial);

        public record OperacaoModel(string CodigoConveniada, TipoOperacao TipoOperacao ,string Prazo, decimal ValorOperacao, decimal Prestacao);

        public record RendimentoModel(string Matricula, decimal ValorRendimento, string Banco, string Agencia, string Conta, string TipoConta, string Conveniada);
        public record ContatoModel(string Ddd, string Telefone, string Email);

        public record NovalPropostaModel(string CpfAgente, string Cpf, ContatoModel Contato, EnderecoModel Endereco, OperacaoModel Operacao, RendimentoModel Rendimento);
        //public record NovalPropostaModel(string CpfAgente, string Cpf, string Nome, DateTime DataNascimento, string Ddd1, string Telefone1, string email,
        //    string Endereco, string Numero, string UF, string Cidade, string Conveniada, string TipoOperacao, string Prazo, decimal ValorOperacao, decimal Prestacao,
        //    string Matricula, decimal ValorRendimento, string Banco, string Agencia, string Conta, string TipoConta);


        public PropostaController()
        {
        }

        [HttpPost(Name = "gerar-proposta")]
        public async Task<IActionResult> GerarProposta(
            [FromBody] NovalPropostaModel input,
            [FromServices] PropostaRepositorio propostaRepositorio,
            CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(input.Cpf))
                return BadRequest("CPF é obrigatório");

            if(input.Rendimento is null)
                return BadRequest("Dados de rendimento é obrigatório");

            if (input.Endereco is null)
                return BadRequest("Endereço é obrigatorio");

            if (input.Contato is null)
                return BadRequest("Dados de contato é obrigatório");

            //consultar as propostas por cpf
            //verificar se exite proposta com situacao em aberto
            if (await propostaRepositorio.ExistePropostaEmAberto(input.Cpf))
                return BadRequest("Possui propostas em aberto");

            //consultar cpf bloqueados
            //verificar se cpf não está bloqueado
            var cliente = await propostaRepositorio.RecuperarCliente(input.Cpf, cancellationToken);
            if(cliente.HasNoValue)
                return BadRequest("Cliente não encontrado");

            if (cliente.Value.Bloqueado)
                return BadRequest("Cpf bloqueado");

            //consultar os agentes
            //verificar se o agente está ativo
            if (await propostaRepositorio.VerificarAgenteInativo(input.Cpf))
                return BadRequest("Agente inativo bloqueado");

            //consultar conveniadas 
            //verificar se a minha proposta é do tipo refinanciamento e se é aceita pelo convenio
            var conveniada = await propostaRepositorio.RecuperarConveniada(input.Operacao.CodigoConveniada, cancellationToken);
            if (conveniada.HasNoValue)
                return BadRequest("Conveniada inválida");

            //Criar Proposta
            var proposta = new Proposta();
            proposta.Cliente = input.Cpf;
            proposta.CpfAgente = input.CpfAgente;
            proposta.DDD = input.Contato.Ddd;
            proposta.Telefone = input.Contato.Telefone;
            proposta.Email = input.Contato.Email;
            proposta.Matricula = input.Rendimento.Matricula;
            proposta.ValorRendimento = input.Rendimento.ValorRendimento;
            proposta.CodigoConveniada = input.Operacao.CodigoConveniada;
            proposta.TipoOperacao = input.Operacao.TipoOperacao;
            proposta.Prazo = input.Operacao.Prazo;
            proposta.ValorOperacao = input.Operacao.ValorOperacao;
            proposta.Endereco = input.Endereco.EnderecoResidencial;
            proposta.Numero = input.Endereco.NumeroResidencial;
            proposta.Cidade = input.Endereco.CidadeResidencial;
            proposta.UF = input.Endereco.UfResidencial;

            if (input.Operacao.TipoOperacao == TipoOperacao.Refinanciamento && !conveniada.Value.AceitaRefinanciamento)
                return BadRequest("Conveniada não aceita Refinanciamento");

            //validar restriçao de valores de acordo com o convenio
            if(!conveniada.Value.Restricoes.IsNullOrEmpty() && conveniada.Value.Restricoes.Any(restricao => restricao.Uf == input.Endereco.UfResidencial && input.Operacao.ValorOperacao > restricao.ValorLimite))
                return BadRequest("Conveniada com restricao para o valor solicitado");

            

            //somar a quantidade de parcelas a idade <= 80 anos
            var dataFutura = DateTime.Now.AddMonths(Convert.ToInt32(input.Operacao.Prazo));
            var diferencaEmAnos = dataFutura.Year - cliente.Value.DataNascimento.Year;

            if (dataFutura < cliente.Value.DataNascimento.AddYears(diferencaEmAnos))
                diferencaEmAnos--;

            if(diferencaEmAnos > 80)
                return BadRequest("Idade ao realizar a ultima parcela excede de 80 anos");

            //Salvar Proposta
            await propostaRepositorio.Adicionar(proposta, cancellationToken);
            await propostaRepositorio.Save();

            //retornar algo
            return Ok();
        }
    }
}
