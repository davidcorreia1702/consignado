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
        List<string> ufs = new List<string> {"SP","RJ","MG","ES","DF","BA","RS","PR" };

        public record MovaPropostaModel(string CpfAgente, string Cpf, string Ddd, string Telefone, string Email, string CepResidencial, string EnderecoResidencial, 
            int NumeroResidencial, string CidadeResidencial, string UfResidencial, string CodigoConveniada, TipoOperacao TipoOperacao, string Prazo, decimal ValorOperacao, 
            decimal Prestacao, string Matricula, decimal ValorRendimento, string Banco, string Agencia, string Conta, string TipoConta);


        public PropostaController()
        {
        }

        [HttpPost(Name = "gerar-proposta")]
        public async Task<IActionResult> GerarProposta(
            [FromBody] MovaPropostaModel input,
            [FromServices] PropostaRepositorio propostaRepositorio,
            CancellationToken cancellationToken)
        {
            if(string.IsNullOrEmpty(input.Cpf))
                return BadRequest("CPF é obrigatório");

            if(input.Matricula is null || input.ValorRendimento <= 0 || string.IsNullOrEmpty(input.Banco) || string.IsNullOrEmpty(input.Agencia) || string.IsNullOrEmpty(input.Conta) || string.IsNullOrEmpty(input.TipoConta))
                return BadRequest("Dados de rendimento é obrigatório");

            if (string.IsNullOrEmpty(input.EnderecoResidencial) || input.NumeroResidencial <= 0 || string.IsNullOrEmpty(input.CidadeResidencial) || string.IsNullOrEmpty(input.UfResidencial))
                return BadRequest("Endereço é obrigatorio");

            if (string.IsNullOrEmpty(input.Email) || string.IsNullOrEmpty(input.Telefone) || string.IsNullOrEmpty(input.Ddd))
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
            var conveniada = await propostaRepositorio.RecuperarConveniada(input.CodigoConveniada, cancellationToken);
            if (conveniada.HasNoValue)
                return BadRequest("Conveniada inválida");

            //Criar Proposta
            var proposta = new Proposta();
            proposta.Cliente = input.Cpf;
            proposta.CpfAgente = input.CpfAgente;
            proposta.DDD = input.Ddd;
            proposta.Telefone = input.Telefone;
            proposta.Email = input.Email;
            proposta.Matricula = input.Matricula;
            proposta.ValorRendimento = input.ValorRendimento;
            proposta.CodigoConveniada = input.CodigoConveniada;
            proposta.TipoOperacao = input.TipoOperacao;
            proposta.Prazo = input.Prazo;
            proposta.ValorOperacao = input.ValorOperacao;
            proposta.Endereco = input.EnderecoResidencial;
            proposta.Numero = input.NumeroResidencial;
            proposta.Cidade = input.CidadeResidencial;
            proposta.UF = input.UfResidencial;

            if (input.TipoOperacao == TipoOperacao.Refinanciamento && !conveniada.Value.AceitaRefinanciamento)
                return BadRequest("Conveniada não aceita Refinanciamento");

            //validar restriçao de valores de acordo com o convenio
            if(!conveniada.Value.Restricoes.IsNullOrEmpty() && conveniada.Value.Restricoes.Any(restricao => restricao.Uf == input.UfResidencial && input.ValorOperacao > restricao.ValorLimite))
                return BadRequest("Conveniada com restricao para o valor solicitado");

            

            //somar a quantidade de parcelas a idade <= 80 anos
            var dataFutura = DateTime.Now.AddMonths(Convert.ToInt32(input.Prazo));
            var diferencaEmAnos = dataFutura.Year - cliente.Value.DataNascimento.Year;

            if (dataFutura < cliente.Value.DataNascimento.AddYears(diferencaEmAnos))
                diferencaEmAnos--;

            if(diferencaEmAnos > 80)
                return BadRequest("Idade ao realizar a ultima parcela excede de 80 anos");


            proposta.TipoAssinatura = (input.Ddd, input.UfResidencial) switch
            {
                _ when ufs.Contains(input.UfResidencial) => TipoAssinatura.Hibrida,    // Assinatura Híbrida se a UF estiver na lista
                _ when input.Ddd == input.UfResidencial => TipoAssinatura.Eletronica,  // Assinatura Eletrônica se o DDD for igual à UF de Residência
                _ => TipoAssinatura.Figital                                               // Assinatura Figital se DDD for diferente da UF de Residência
            };

            //Salvar Proposta
            await propostaRepositorio.Adicionar(proposta, cancellationToken);
            await propostaRepositorio.Save();

            //retornar algo
            return Ok();
        }
    }
}
