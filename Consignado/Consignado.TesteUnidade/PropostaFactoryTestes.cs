using Consignado.HttpApi.Dominio.Aplicacao;
using Consignado.HttpApi.Dominio.Entidade;
using Consignado.HttpApi.Dominio.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Consignado.Controllers.PropostaController;

namespace Consignado.TesteUnidade
{
    public class PropostaFactoryTestes
    {
        private readonly PropostaFactory _propostaFactory;

        public PropostaFactoryTestes()
        {
            _propostaFactory = new PropostaFactory();
        }

        [Fact]
        public void Gravar_QuandoSatisfazerTodasAsRegrasEUfDevemTerAssinaturaHibrida_DeveSalvar()
        {
            // Arrange
            var conveniada = new Conveniada
            {
                Codigo = "0020",
                Id = 1,
                Nome = "INSS",
                AceitaRefinanciamento = true,
                Restricoes = new List<ConveniadaRestricao>
                    {
                        new ConveniadaRestricao { Uf = "SP", ValorLimite = 100000 },
                        new ConveniadaRestricao { Uf = "RS", ValorLimite = 500000 }
                    }
            };

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
                TipoConta: "CC"
            ));

            // Act
            var resultado = _propostaFactory.Gravar(command, conveniada);

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
            Assert.Equal("0020", proposta.CodigoConveniada);
            Assert.Equal(TipoOperacao.Novo, proposta.TipoOperacao);
            Assert.Equal("987654321", proposta.Matricula);
            Assert.Equal(3000, proposta.ValorRendimento);
            Assert.Equal("24", proposta.Prazo);
            Assert.Equal(15000, proposta.ValorOperacao);
            Assert.Equal(625, proposta.Prestacao);
            Assert.Equal("001", proposta.Banco);
            Assert.Equal("1234", proposta.Agencia);
            Assert.Equal("56789-0", proposta.Conta);
            Assert.Equal("CC", proposta.TipoConta);
            Assert.Equal(TipoAssinatura.Hibrida, proposta.TipoAssinatura);
        }

        [Fact]
        public void Gravar_QuandoSatisfazerTodasAsRegrasEUfsIguas_DeveSalvar()
        {
            // Arrange
            var conveniada = new Conveniada
            {
                Codigo = "0020",
                Id = 1,
                Nome = "INSS",
                AceitaRefinanciamento = true,
                Restricoes = new List<ConveniadaRestricao>
                    {
                        new ConveniadaRestricao { Uf = "SP", ValorLimite = 100000 },
                        new ConveniadaRestricao { Uf = "RS", ValorLimite = 500000 }
                    }
            };

            var command = new GravarPropostaCommand(new MovaPropostaModel(
                CpfAgente: "12345678901",
                Cpf: "12345678901",
                DataNascimento: new DateTime(1980, 1, 1),
                DDD: "21",
                Telefone: "999999999",
                Email: "test@example.com",
                Cep: "12345-678",
                Endereco: "Rua Teste",
                Numero: "123",
                Cidade: "Rio de Janeiro",
                Uf: "RJ",
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
                TipoConta: "CC"
            ));

            // Act
            var resultado = _propostaFactory.Gravar(command, conveniada);

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
            Assert.Equal("0020", proposta.CodigoConveniada);
            Assert.Equal(TipoOperacao.Novo, proposta.TipoOperacao);
            Assert.Equal("987654321", proposta.Matricula);
            Assert.Equal(3000, proposta.ValorRendimento);
            Assert.Equal("24", proposta.Prazo);
            Assert.Equal(15000, proposta.ValorOperacao);
            Assert.Equal(625, proposta.Prestacao);
            Assert.Equal("001", proposta.Banco);
            Assert.Equal("1234", proposta.Agencia);
            Assert.Equal("56789-0", proposta.Conta);
            Assert.Equal("CC", proposta.TipoConta);
            Assert.Equal(TipoAssinatura.Eletronica, proposta.TipoAssinatura);
        }

        [Fact]
        public void Gravar_QuandoSatisfazerTodasAsRegrasEUfsDiferentes_DeveSalvar()
        {
            // Arrange
            var conveniada = new Conveniada
            {
                Codigo = "0020",
                Id = 1,
                Nome = "INSS",
                AceitaRefinanciamento = true,
                Restricoes = new List<ConveniadaRestricao>
                    {
                        new ConveniadaRestricao { Uf = "SP", ValorLimite = 100000 },
                        new ConveniadaRestricao { Uf = "RS", ValorLimite = 500000 }
                    }
            };

            var command = new GravarPropostaCommand(new MovaPropostaModel(
                CpfAgente: "12345678901",
                Cpf: "12345678901",
                DataNascimento: new DateTime(1980, 1, 1),
                DDD: "21",
                Telefone: "999999999",
                Email: "test@example.com",
                Cep: "12345-678",
                Endereco: "Rua Teste",
                Numero: "123",
                Cidade: "São Paulo",
                Uf: "PE",
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
                TipoConta: "CC"
            ));

            // Act
            var resultado = _propostaFactory.Gravar(command, conveniada);

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
            Assert.Equal("São Paulo", proposta.Cidade);
            Assert.Equal("PE", proposta.Uf);
            Assert.Equal("0020", proposta.CodigoConveniada);
            Assert.Equal(TipoOperacao.Novo, proposta.TipoOperacao);
            Assert.Equal("987654321", proposta.Matricula);
            Assert.Equal(3000, proposta.ValorRendimento);
            Assert.Equal("24", proposta.Prazo);
            Assert.Equal(15000, proposta.ValorOperacao);
            Assert.Equal(625, proposta.Prestacao);
            Assert.Equal("001", proposta.Banco);
            Assert.Equal("1234", proposta.Agencia);
            Assert.Equal("56789-0", proposta.Conta);
            Assert.Equal("CC", proposta.TipoConta);
            Assert.Equal(TipoAssinatura.Figital, proposta.TipoAssinatura);
        }

        [Fact]
        public void GravarPropostaRefinanciamento_QuandoConveniadaNaoPermiteRefinanciamento_DeveFalhar()
        {
            // Arrange
            var conveniada = new Conveniada
            {
                Codigo = "0020",
                Id = 1,
                Nome = "INSS",
                AceitaRefinanciamento = false,
                Restricoes = new List<ConveniadaRestricao>
                    {
                        new ConveniadaRestricao { Uf = "SP", ValorLimite = 100000 },
                        new ConveniadaRestricao { Uf = "RS", ValorLimite = 500000 }
                    }
            };

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
                TipoOperacao: TipoOperacao.Refinanciamento,
                Matricula: "987654321",
                ValorRendimento: 3000,
                Prazo: "24",
                ValorOperacao: 15000,
                Prestacao: 625,
                Banco: "001",
                Agencia: "1234",
                Conta: "56789-0",
                TipoConta: "CC"
            ));

            // Act
            var resultado = _propostaFactory.Gravar(command, conveniada);

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Conveniada não aceita Refinanciamento", resultado.Error);
        }

        [Fact]
        public void Gravar_QuandoValorOperacaoExcedeRestricaoValorLimiteConveniada_DeveFalhar()
        {
            // Arrange
            var conveniada = new Conveniada
            {
                Codigo = "0020",
                Id = 1,
                Nome = "INSS",
                AceitaRefinanciamento = true,
                Restricoes = new List<ConveniadaRestricao>
                    {
                        new ConveniadaRestricao { Uf = "SP", ValorLimite = 10000 },
                        new ConveniadaRestricao { Uf = "RS", ValorLimite = 500000 }
                    }
            };

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
                TipoConta: "CC"
            ));

            // Act
            var resultado = _propostaFactory.Gravar(command, conveniada);

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Conveniada com restricao para o valor solicitado", resultado.Error);
        }

        [Fact]
        public void Gravar_QuandoIdadeUltrapassa80AnosUltimaParcela_DeveFalhar()
        {
            // Arrange
            var conveniada = new Conveniada
            {
                Codigo = "0020",
                Id = 1,
                Nome = "INSS",
                AceitaRefinanciamento = true,
                Restricoes = new List<ConveniadaRestricao>
                    {
                        new ConveniadaRestricao { Uf = "SP", ValorLimite = 100000 },
                        new ConveniadaRestricao { Uf = "RS", ValorLimite = 500000 }
                    }
            };

            var command = new GravarPropostaCommand(new MovaPropostaModel(
                CpfAgente: "12345678901",
                Cpf: "12345678901",
                DataNascimento: new DateTime(1945, 1, 1),
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
                TipoConta: "CC"
            ));

            // Act
            var resultado = _propostaFactory.Gravar(command, conveniada);

            // Assert
            Assert.True(resultado.IsFailure);
            Assert.Equal("Idade ao realizar a ultima parcela excede de 80 anos", resultado.Error);
        }
    }
}
