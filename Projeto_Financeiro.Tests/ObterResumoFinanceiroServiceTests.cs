using Moq;
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Tests
{
    public class ObterResumoFinanceiroServiceTests
    {
        private readonly Mock<IRelatorioRepository> _repositoryMock;
        private readonly ObterResumoFinanceiroService _service;

        public ObterResumoFinanceiroServiceTests()
        {
            _repositoryMock = new Mock<IRelatorioRepository>();
            _service = new ObterResumoFinanceiroService(_repositoryMock.Object);
        }

        [Fact(DisplayName = "Deve retornar resumo financeiro com dados válidos")]
        public async Task spResumoAsync_DeveRetornarResumoFinanceiro_ComDados()
        {
            // Arrange
            var dataInicio = new DateTime(2023, 1, 1);
            var dataFim = new DateTime(2023, 1, 31);

            var resumoEsperado = new List<ResumoFinanceiro>
            {
                new ResumoFinanceiro
                {
                    SaldoTotal = 12000.00m,
                    TotalReceitas = 20000.00m,
                    TotalDespesas = 8000.00m,
                }
            };

            _repositoryMock
                .Setup(r => r.ObterResumoAsync(dataInicio, dataFim))
                .ReturnsAsync(resumoEsperado);

            // Act
            var resultado = await _service.spResumoAsync(dataInicio, dataFim);

            // Assert
            Assert.NotNull(resultado);
            Assert.Single(resultado);
            Assert.Equal(12000.00m, resultado[0].SaldoTotal);
            Assert.Equal(20000.00m, resultado[0].TotalReceitas);
            Assert.Equal(8000.00m, resultado[0].TotalDespesas);
        }

        [Fact(DisplayName = "Deve retornar lista vazia quando não houver lançamentos")]
        public async Task spResumoAsync_DeveRetornarListaVazia_QuandoSemDados()
        {
            // Arrange
            var dataInicio = new DateTime(2024, 1, 1);
            var dataFim = new DateTime(2024, 1, 31);

            _repositoryMock
                .Setup(r => r.ObterResumoAsync(dataInicio, dataFim))
                .ReturnsAsync(new List<ResumoFinanceiro>());

            // Act
            var resultado = await _service.spResumoAsync(dataInicio, dataFim);

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        [Fact(DisplayName = "Deve lançar exceção em caso de falha no repositório")]
        public async Task spResumoAsync_DeveLancarExcecao_QuandoRepositorioFalha()
        {
            // Arrange
            var dataInicio = DateTime.Today.AddDays(-30);
            var dataFim = DateTime.Today;

            _repositoryMock
                .Setup(r => r.ObterResumoAsync(dataInicio, dataFim))
                .ThrowsAsync(new Exception("Erro de banco"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.spResumoAsync(dataInicio, dataFim));

            Assert.Equal("Erro de banco", ex.Message);
        }
    }
}
