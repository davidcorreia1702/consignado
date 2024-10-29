using CSharpFunctionalExtensions;

namespace Consignado.HttpApi.Dominio.Propostas.Aplicacao
{
    public class GravarPropostaValidador
    {
        public Result Validar(GravarPropostaCommand command)
        {
            if (string.IsNullOrEmpty(command.CpfAgente)) return Result.Failure("CpfAgente é obrigatório.");
            if (string.IsNullOrEmpty(command.Cpf)) return Result.Failure("Cpf é obrigatório.");
            if (string.IsNullOrEmpty(command.DDD)) return Result.Failure("DDD é obrigatório.");
            if (string.IsNullOrEmpty(command.Telefone)) return Result.Failure("Telefone é obrigatório.");
            if (string.IsNullOrEmpty(command.Email)) return Result.Failure("Email é obrigatório.");
            if (string.IsNullOrEmpty(command.Endereco)) return Result.Failure("Endereço é obrigatório.");
            if (string.IsNullOrEmpty(command.Numero)) return Result.Failure("Número é obrigatório.");
            if (string.IsNullOrEmpty(command.Cidade)) return Result.Failure("Cidade é obrigatória.");
            if (string.IsNullOrEmpty(command.Uf)) return Result.Failure("UF é obrigatório.");
            if (string.IsNullOrEmpty(command.CodigoConveniada)) return Result.Failure("Código da Conveniada é obrigatório.");
            if (string.IsNullOrEmpty(command.Prazo)) return Result.Failure("Prazo deve ser maior que zero.");
            if (command.ValorOperacao <= 0) return Result.Failure("Valor da Operação deve ser maior que zero.");
            if (command.Prestacao <= 0) return Result.Failure("Prestação deve ser maior que zero.");
            if (string.IsNullOrEmpty(command.Matricula)) return Result.Failure("Matrícula é obrigatória.");
            if (command.ValorRendimento <= 0) return Result.Failure("Valor do Rendimento deve ser maior que zero.");
            if (string.IsNullOrEmpty(command.Banco)) return Result.Failure("Banco é obrigatório.");
            if (string.IsNullOrEmpty(command.Agencia)) return Result.Failure("Agência é obrigatória.");
            if (string.IsNullOrEmpty(command.Conta)) return Result.Failure("Conta é obrigatória.");

            return Result.Success();
        }
    }
}
