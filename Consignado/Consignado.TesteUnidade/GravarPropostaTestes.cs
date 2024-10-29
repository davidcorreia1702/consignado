using Consignado.HttpApi.Dominio.Inscricao;
using Consignado.HttpApi.Dominio.Inscricao.Aplicacao;
using Consignado.HttpApi.Dominio.Inscricao.Infraestrutura;
using Moq;
using static Consignado.Controllers.PropostaController;

namespace Consignado.TesteUnidade
{
    public class GravarPropostaTestes
    {
        [Fact]
        public async Task GravarProposta_QuandoSatisfazerTodasAsRegras_DeveSalvar()
        {
            //Arrange
            var mockRepositorio = new Mock<IPropostaRepositorio>();

            mockRepositorio
                .Setup(n => n.ExistePropostaEmAberto(It.IsAny<string>()))
                .ReturnsAsync(false);
            mockRepositorio.Setup(n => n.VerificarCpfBloqueado(It.IsAny<string>()))
                .ReturnsAsync(false);
            mockRepositorio.Setup(n => n.VerificarAgenteInativo(It.IsAny<string>()))
                .ReturnsAsync(false);

            var conveniada = new Conveniada(1, "INSS", "0020", aceitaRefinanciamento: true);
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("SP", 100000));
            conveniada.AdicionarRestricao(new ConveniadaUfRestricao("RS", 500000 ));
            mockRepositorio.Setup(n => n.RecuperarConveniada(It.IsAny<string>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(conveniada);

            mockRepositorio
                .Setup(r => r.Adicionar(It.IsAny<Proposta>(), It.IsAny<CancellationToken>()))
                .Returns(Task.CompletedTask);
            mockRepositorio.Setup(r => r.Save()).Returns(Task.CompletedTask);

            var handler = new GravarPropostaHandler(mockRepositorio.Object);

            var command = new GravarPropostaCommand(new MovaPropostaModel(
                CpfAgente: "12345678901",
                Cpf: "12345678901",
                DataNascimento: new DateTime(1980, 1, 1),
                DDD: "11",
                Telefone: "999999999",
                Email: "test@example.com",
                Cep: "12345-678",
                Endereco: "Rua Teste",
                Numero: "123",
                Cidade: "São Paulo",
                Uf: "SP",
                CodigoConveniada: "0020",
                TipoOperacao: TipoOperacao.Novo,
                Matricula: "987654321",
                ValorRendimento: 3000,
                Prazo: "24",
                ValorOperacao: 15000,
                Prestacao: 625,
                Banco: "001",
                Agencia: "1234",
                Conta: "56789-0",
                TipoConta: Tipoconta.ContaCorrente
            ));

            //Act
            var resultado = await handler.Handle(command, CancellationToken.None);

            //Assert
            Assert.True(resultado.IsSuccess);
            Assert.NotNull(resultado.Value);

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
            Assert.Equal(1, proposta.ConveniadaId);
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
        }
    }
}