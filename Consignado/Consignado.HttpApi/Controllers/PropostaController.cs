using Consignado.HttpApi.Dominio.Aplicacao;
using Consignado.HttpApi.Dominio.Entidade;
using Consignado.HttpApi.Dominio.Infraestrutura;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Consignado.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PropostaController : ControllerBase
    {
        public record MovaPropostaModel(string CpfAgente, string Cpf, DateTime DataNascimento ,string DDD, string Telefone, string Email, string Cep, string Endereco, 
            string Numero, string Cidade, string Uf, string CodigoConveniada, TipoOperacao TipoOperacao, string Prazo, decimal ValorOperacao, 
            decimal Prestacao, string Matricula, decimal ValorRendimento, string Banco, string Agencia, string Conta, string TipoConta);

        [HttpPost(Name = "gerar-proposta")]
        public async Task<IActionResult> GerarProposta(
            [FromBody] MovaPropostaModel input,
            [FromServices] PropostaRepositorio propostaRepositorio,
            [FromServices] GravarPropostaHandler handler,
            CancellationToken cancellationToken)
        {

            var command = new GravarPropostaCommand(input);

            var validationResults = new List<ValidationResult>();
            var validationContext = new ValidationContext(command);

            bool isValid = Validator.TryValidateObject(command, validationContext, validationResults, true);

            if (!isValid)
            {
                var erros = validationResults.Select(vr => vr.ErrorMessage).ToList();
                return BadRequest(string.Join("; ", erros));
            }

            var result = await handler.Handle(command, cancellationToken);

            return result.IsSuccess
                ? Ok(result.Value)
                : BadRequest(result.Error);
        }
    }
}
