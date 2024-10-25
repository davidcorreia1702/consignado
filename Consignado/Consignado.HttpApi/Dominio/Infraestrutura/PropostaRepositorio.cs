using Consignado.HttpApi.Dominio.Entidade;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Dapper;
using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Infraestrutura
{
    public class PropostaRepositorio
    {
        private readonly PropostaDbContext dbContext;

        public PropostaRepositorio(PropostaDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<bool> ExistePropostaEmAberto(string cpf)
        {
            return await dbContext.Database.GetDbConnection()
                                .QueryFirstOrDefaultAsync<bool>(
                                    "SELECT CASE WHEN EXISTS (SELECT 1 FROM Consignado.Proposta WHERE cpf = @cpf AND situacao = 'Em Aberto') THEN 1 ELSE 0 END",
                                    new { cpf }
                                );
        }

        public async Task<bool> VerificarCpfBloqueado(string cpf)
        {
            return await dbContext.Database.GetDbConnection()
                                .QueryFirstOrDefaultAsync<bool>(
                                    "SELECT CASE WHEN EXISTS (SELECT 1 FROM Consignado.Cliente WHERE cpf = @cpf AND bloqueado = 1) THEN 1 ELSE 0 END",
                                    new { cpf }
                                );
        }

        public async Task<bool> VerificarAgenteInativo(string cpfAgente)
        {
            return await dbContext.Database.GetDbConnection()
                                .QueryFirstOrDefaultAsync<bool>(
                                    "SELECT CASE WHEN EXISTS (SELECT 1 FROM Consignado.Agente WHERE cpf = @cpf AND ativo = 0) THEN 1 ELSE 0 END",
                                    new { cpfAgente }
                                );
        }

        public async Task<Maybe<Conveniada>> RecuperarConveniada(string codigo, CancellationToken cancellationToken)
        {
            var conveniada = await dbContext.Conveniadas
                                                .Include(n => n.Restricoes)
                                                .FirstOrDefaultAsync(c => c.Codigo == codigo, cancellationToken);
            return conveniada ?? Maybe<Conveniada>.None;
        }

        public async Task<Maybe<Cliente>> RecuperarCliente(string cpf, CancellationToken cancellationToken)
        {
            var cliente = await dbContext.Clientes.FirstOrDefaultAsync(c => c.Cpf == cpf, cancellationToken);
            return cliente ?? Maybe<Cliente>.None;
        }

        public async Task Adicionar(Proposta proposta, CancellationToken cancellationToken)
        {
            await dbContext.Propostas.AddAsync(proposta, cancellationToken);
        }

        public Task Save()
        {
            return dbContext.SaveChangesAsync();
        }
    }
}
