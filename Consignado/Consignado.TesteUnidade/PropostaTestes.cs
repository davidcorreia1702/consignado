using Consignado.HttpApi.Dominio.Propostas;
using Consignado.HttpApi.Dominio.Strategies.RegrasPorConveniada;

namespace Consignado.TesteUnidade
{
    public class PropostaTestes
    {
        [Fact]
        public void Gravar_QuandoSatisfazerTodasAsRegrasEUfDevemTerAssinaturaHibrida_DeveSalvar()
        {
            // Arrange
            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: true);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 100000));
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("RS", 500000));

            var unidadeFederativa = new UnidadeFederativa("SP", assinaturaHibrida: true);
            unidadeFederativa.AdicionarDdd(new DDD("11"));

            // Act
            var resultado = Proposta.Criar(
                cpfAgente: "12345678901",
                cpf: "12345678901",
                dataNascimento: new DateTime(1980, 1, 1),
                ddd: "11",
                telefone: "999999999",
                email: "test@example.com",
                cep: "12345-678",
                endereco: "Rua Teste",
                numero: "123",
                cidade: "São Paulo",
                uf: unidadeFederativa,
                tipoOperacao: TipoOperacao.Novo,
                matricula: "987654321",
                valorRendimento: 3000,
                prazo: "24",
                valorOperacao: 15000,
                prestacao: 625,
                banco: "001",
                agencia: "1234",
                conta: "56789-0",
                tipoConta: Tipoconta.ContaCorrente, 
                conveniada,
                ufDdd: unidadeFederativa.Sigla,
                obterRegrasBasicas());

            // Assert
            Assert.True(resultado.IsSuccess);
            var proposta = resultado.Value;
            Assert.Equal("12345678901", proposta.CpfAgente);
            Assert.Equal("12345678901", proposta.Cpf);
            Assert.Equal(new DateTime(1980, 1, 1), proposta.DataNascimento);
            Assert.Equal("11", proposta.DDD);
            Assert.Equal("999999999", proposta.Telefone);
            Assert.Equal("test@example.com", proposta.Email);
            Assert.Equal("12345-678", proposta.Cep);
            Assert.Equal("Rua Teste", proposta.Endereco);
            Assert.Equal("123", proposta.Numero);
            Assert.Equal("São Paulo", proposta.Cidade);
            Assert.Equal("SP", proposta.Uf);
            Assert.Equal(conveniada.Id, proposta.ConveniadaId);
            Assert.Equal(TipoOperacao.Novo, proposta.TipoOperacao);
            Assert.Equal("987654321", proposta.Matricula);
            Assert.Equal(3000, proposta.ValorRendimento);
            Assert.Equal("24", proposta.Prazo);
            Assert.Equal(15000, proposta.ValorOperacao);
            Assert.Equal(625, proposta.Prestacao);
            Assert.Equal("001", proposta.Banco);
            Assert.Equal("1234", proposta.Agencia);
            Assert.Equal("56789-0", proposta.Conta);
            Assert.Equal(Tipoconta.ContaCorrente, proposta.TipoConta);
            Assert.Equal(TipoAssinatura.Hibrida, proposta.TipoAssinatura);
        }

        [Fact]
        public void Gravar_QuandoSatisfazerTodasAsRegrasEUfsIguas_DeveSalvar()
        {
            // Arrange
            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: true);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 100000));

            var unidadeFederativa = new UnidadeFederativa("RJ", assinaturaHibrida: false);
            unidadeFederativa.AdicionarDdd(new DDD("21"));

            // Act
            var resultado = Proposta.Criar(
                cpfAgente: "12345678901",
                cpf: "12345678901",
                dataNascimento: new DateTime(1980, 1, 1),
                ddd: "21",
                telefone: "999999999",
                email: "test@example.com",
                cep: "12345-678",
                endereco: "Rua Teste",
                numero: "123",
                cidade: "Rio de Janeiro",
                uf: unidadeFederativa,
                tipoOperacao: TipoOperacao.Novo,
                matricula: "987654321",
                valorRendimento: 3000,
                prazo: "24",
                valorOperacao: 15000,
                prestacao: 625,
                banco: "001",
                agencia: "1234",
                conta: "56789-0",
                tipoConta: Tipoconta.ContaCorrente,
                conveniada,
                ufDdd: unidadeFederativa.Sigla,
                obterRegrasBasicas());

            // Assert
            Assert.True(resultado.IsSuccess);
            var proposta = resultado.Value;
            Assert.Equal("12345678901", proposta.CpfAgente);
            Assert.Equal("12345678901", proposta.Cpf);
            Assert.Equal(new DateTime(1980, 1, 1), proposta.DataNascimento);
            Assert.Equal("21", proposta.DDD);
            Assert.Equal("999999999", proposta.Telefone);
            Assert.Equal("test@example.com", proposta.Email);
            Assert.Equal("12345-678", proposta.Cep);
            Assert.Equal("Rua Teste", proposta.Endereco);
            Assert.Equal("123", proposta.Numero);
            Assert.Equal("Rio de Janeiro", proposta.Cidade);
            Assert.Equal("RJ", proposta.Uf);
            Assert.Equal(conveniada.Id, proposta.ConveniadaId);
            Assert.Equal(TipoOperacao.Novo, proposta.TipoOperacao);
            Assert.Equal("987654321", proposta.Matricula);
            Assert.Equal(3000, proposta.ValorRendimento);
            Assert.Equal("24", proposta.Prazo);
            Assert.Equal(15000, proposta.ValorOperacao);
            Assert.Equal(625, proposta.Prestacao);
            Assert.Equal("001", proposta.Banco);
            Assert.Equal("1234", proposta.Agencia);
            Assert.Equal("56789-0", proposta.Conta);
            Assert.Equal(Tipoconta.ContaCorrente, proposta.TipoConta);
            Assert.Equal(TipoAssinatura.Eletronica, proposta.TipoAssinatura);
        }

        [Fact]
        public void Gravar_QuandoSatisfazerTodasAsRegrasEUfsDiferentes_DeveSalvar()
        {
            // Arrange
            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: true);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 100000));

            var unidadeFederativa = new UnidadeFederativa("RJ", assinaturaHibrida: false);
            unidadeFederativa.AdicionarDdd(new DDD("21"));

            var unidadeFederativaNascimento = new UnidadeFederativa("PE", assinaturaHibrida: false);

            // Act
            var resultado = Proposta.Criar(
                cpfAgente: "12345678901",
                cpf: "12345678901",
                dataNascimento: new DateTime(1980, 1, 1),
                ddd: "21",
                telefone: "999999999",
                email: "test@example.com",
                cep: "12345-678",
                endereco: "Rua Teste",
                numero: "123",
                cidade: "Recife",
                uf: unidadeFederativaNascimento,
                tipoOperacao: TipoOperacao.Novo,
                matricula: "987654321",
                valorRendimento: 3000,
                prazo: "24",
                valorOperacao: 15000,
                prestacao: 625,
                banco: "001",
                agencia: "1234",
                conta: "56789-0",
                tipoConta: Tipoconta.ContaCorrente,
                conveniada,
                ufDdd: unidadeFederativa.Sigla,
                obterRegrasBasicas());

            // Assert
            Assert.True(resultado.IsSuccess);
            var proposta = resultado.Value;
            Assert.Equal("12345678901", proposta.CpfAgente);
            Assert.Equal("12345678901", proposta.Cpf);
            Assert.Equal(new DateTime(1980, 1, 1), proposta.DataNascimento);
            Assert.Equal("21", proposta.DDD);
            Assert.Equal("999999999", proposta.Telefone);
            Assert.Equal("test@example.com", proposta.Email);
            Assert.Equal("12345-678", proposta.Cep);
            Assert.Equal("Rua Teste", proposta.Endereco);
            Assert.Equal("123", proposta.Numero);
            Assert.Equal("Recife", proposta.Cidade);
            Assert.Equal("PE", proposta.Uf);
            Assert.Equal(conveniada.Id, proposta.ConveniadaId);
            Assert.Equal(TipoOperacao.Novo, proposta.TipoOperacao);
            Assert.Equal("987654321", proposta.Matricula);
            Assert.Equal(3000, proposta.ValorRendimento);
            Assert.Equal("24", proposta.Prazo);
            Assert.Equal(15000, proposta.ValorOperacao);
            Assert.Equal(625, proposta.Prestacao);
            Assert.Equal("001", proposta.Banco);
            Assert.Equal("1234", proposta.Agencia);
            Assert.Equal("56789-0", proposta.Conta);
            Assert.Equal(Tipoconta.ContaCorrente, proposta.TipoConta);
            Assert.Equal(TipoAssinatura.Figital, proposta.TipoAssinatura);
        }

        [Fact]
        public void GravarPropostaRefinanciamento_QuandoConveniadaNaoPermiteRefinanciamento_DeveFalhar()
        {
            // Arrange
            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: false);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 100000));

            var unidadeFederativa = new UnidadeFederativa("SP", assinaturaHibrida: true);
            unidadeFederativa.AdicionarDdd(new DDD("11"));

            //Act
            var resultado = Proposta.Criar(
                cpfAgente: "12345678901",
                cpf: "12345678901",
                dataNascimento: new DateTime(1980, 1, 1),
                ddd: "11",
                telefone: "999999999",
                email: "test@example.com",
                cep: "12345-678",
                endereco: "Rua Teste",
                numero: "123",
                cidade: "São Paulo",
                uf: unidadeFederativa,
                tipoOperacao: TipoOperacao.Refinanciamento,
                matricula: "987654321",
                valorRendimento: 3000,
                prazo: "24",
                valorOperacao: 15000,
                prestacao: 625,
                banco: "001",
                agencia: "1234",
                conta: "56789-0",
                tipoConta: Tipoconta.ContaCorrente,
                conveniada,
                ufDdd: unidadeFederativa.Sigla,
                obterRegrasBasicas());

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Conveniada não aceita Refinanciamento", resultado.Error);
        }

        [Fact]
        public void Gravar_QuandoValorOperacaoExcedeRestricaoValorLimiteConveniada_DeveFalhar()
        {
            // Arrange
            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: true);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 10000));

            var unidadeFederativa = new UnidadeFederativa("SP", assinaturaHibrida: true);
            unidadeFederativa.AdicionarDdd(new DDD("11"));

            //Act
            var resultado = Proposta.Criar(
                cpfAgente: "12345678901",
                cpf: "12345678901",
                dataNascimento: new DateTime(1980, 1, 1),
                ddd: "11",
                telefone: "999999999",
                email: "test@example.com",
                cep: "12345-678",
                endereco: "Rua Teste",
                numero: "123",
                cidade: "São Paulo",
                uf: unidadeFederativa,
                tipoOperacao: TipoOperacao.Novo,
                matricula: "987654321",
                valorRendimento: 3000,
                prazo: "24",
                valorOperacao: 15000,
                prestacao: 625,
                banco: "001",
                agencia: "1234",
                conta: "56789-0",
                tipoConta: Tipoconta.ContaCorrente,
                conveniada,
                ufDdd: unidadeFederativa.Sigla,
                obterRegrasBasicas());

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Conveniada com restricao para o valor solicitado", resultado.Error);
        }

        [Fact]
        public void Gravar_QuandoIdadeUltrapassa80AnosUltimaParcela_DeveFalhar()
        {
            // Arrange
            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: true);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 100000));

            var unidadeFederativa = new UnidadeFederativa("SP", assinaturaHibrida: true);
            unidadeFederativa.AdicionarDdd(new DDD("11"));

            // Act
            var resultado = Proposta.Criar(
                cpfAgente: "12345678901",
                cpf: "12345678901",
                dataNascimento: new DateTime(1945, 1, 1),
                ddd: "11",
                telefone: "999999999",
                email: "test@example.com",
                cep: "12345-678",
                endereco: "Rua Teste",
                numero: "123",
                cidade: "São Paulo",
                uf: unidadeFederativa,
                tipoOperacao: TipoOperacao.Novo,
                matricula: "987654321",
                valorRendimento: 3000,
                prazo: "24",
                valorOperacao: 15000,
                prestacao: 625,
                banco: "001",
                agencia: "1234",
                conta: "56789-0",
                tipoConta: Tipoconta.ContaCorrente,
                conveniada,
                ufDdd: unidadeFederativa.Sigla,
                obterRegrasBasicas());

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Idade ao realizar a ultima parcela excede de 80 anos", resultado.Error);
        }

        private IEnumerable<IValidarProposta> obterRegrasBasicas()
        {
            return new List<IValidarProposta>
            {
                new ValidacaoRestricaoValor(),
                new ValidacaoPermiteRefinanciamento(),
                new ValidacaoIdadeMaximaUltimaParcela(),
            };
        }

        private void CriarDadosUnidadeFederativa()
        {
            var unidadeFederativa = new UnidadeFederativa("SP", assinaturaHibrida: true);
            unidadeFederativa.AdicionarDdd(new DDD("11"));

            
        }
    }
}
