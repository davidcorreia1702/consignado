using Consignado.HttpApi.Dominio.Propostas;
using Consignado.HttpApi.Dominio.Propostas.Aplicacao;
using Consignado.HttpApi.Dominio.Propostas.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Consignado.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropostaController : ControllerBase
    {
        public record NovaPropostaModel(string CpfAgente, string Cpf, DateTime DataNascimento ,string DDD, string Telefone, string Email, string Cep, string Endereco, 
            string Numero, string Cidade, string Uf, string CodigoConveniada, TipoOperacao TipoOperacao, string Prazo, decimal ValorOperacao, 
            decimal Prestacao, string Matricula, decimal ValorRendimento, string Banco, string Agencia, string Conta, Tipoconta TipoConta);

        [HttpPost(Name = "gerar-proposta")]
        public async Task<IActionResult> GerarProposta(
            [FromBody] NovaPropostaModel input,
            [FromServices] GravarPropostaHandler handler,
            CancellationToken cancellationToken)
        {

            var command = new GravarPropostaCommand(
                                input.CpfAgente,
                                input.Cpf,
                                input.DDD,
                                input.DataNascimento,
                                input.Telefone,
                                input.Email,
                                input.Cep,
                                input.Endereco,
                                input.Numero,
                                input.Cidade,
                                input.Uf,
                                input.CodigoConveniada,
                                input.TipoOperacao,
                                input.Prazo,
                                input.ValorOperacao,
                                input.Prestacao,
                                input.Matricula,
                                input.ValorRendimento,
                                input.Banco,
                                input.Agencia,
                                input.Conta,
                                input.TipoConta
                            );

            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}
