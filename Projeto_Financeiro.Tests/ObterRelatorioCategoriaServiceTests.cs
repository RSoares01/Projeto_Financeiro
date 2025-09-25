using Moq;
using Xunit;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;
using Projeto_Financeiro.Domain.ReadModel;

namespace Projeto_Financeiro.Tests
{
    public class ObterRelatorioCategoriaServiceTests
    {
        private readonly Mock<IRelatorioRepository> _relatorioRepositoryMock;
        private readonly ObterRelatorioCategoriaService _service;

        public ObterRelatorioCategoriaServiceTests()
        {
            _relatorioRepositoryMock = new Mock<IRelatorioRepository>();
            _service = new ObterRelatorioCategoriaService(_relatorioRepositoryMock.Object);
        }

        [Fact(DisplayName = "Deve retornar relatório de categorias com dados corretos")]
        public async Task spRelatorioCategoriaAsync_DeveRetornarRelatorio_ComDadosValidos()
        {
            // Arrange
            var dataInicio = new DateTime(2023, 01, 01);
            var dataFim = new DateTime(2023, 12, 31);

            var resultadoEsperado = new List<RelatorioCategoria>
            {
                new RelatorioCategoria
                {
                    CategoriaId = 1,
                    CategoriaNome = "Receita de Vendas",
                    CategoriaTipo = "R",
                    QuantidadeMovimentos = 10,
                    SomaValores = 150000.00m
                },
                new RelatorioCategoria
                {
                    CategoriaId = 2,
                    CategoriaNome = "Despesas com Pessoal",
                    CategoriaTipo = "D",
                    QuantidadeMovimentos = 5,
                    SomaValores = 45000.00m
                }
            };

            _relatorioRepositoryMock
                .Setup(r => r.ObterRelatorioByCategoriaAsync(dataInicio, dataFim))
                .ReturnsAsync(resultadoEsperado);

            // Act
            var resultado = await _service.spRelatorioCategoriaAsync(dataInicio, dataFim);

            // Assert
            Assert.NotNull(resultado);
            Assert.Equal(2, resultado.Count);
            Assert.Contains(resultado, r => r.CategoriaNome == "Receita de Vendas" && r.CategoriaTipo == "R");
            Assert.Contains(resultado, r => r.CategoriaNome == "Despesas com Pessoal" && r.CategoriaTipo == "D");
        }

        [Fact(DisplayName = "Deve retornar lista vazia quando não houver dados")]
        public async Task spRelatorioCategoriaAsync_DeveRetornarVazio_QuandoSemMovimentos()
        {
            // Arrange
            var dataInicio = new DateTime(2025, 01, 01);
            var dataFim = new DateTime(2025, 12, 31);

            _relatorioRepositoryMock
                .Setup(r => r.ObterRelatorioByCategoriaAsync(dataInicio, dataFim))
                .ReturnsAsync(new List<RelatorioCategoria>());

            // Act
            var resultado = await _service.spRelatorioCategoriaAsync(dataInicio, dataFim);

            // Assert
            Assert.NotNull(resultado);
            Assert.Empty(resultado);
        }

        [Fact(DisplayName = "Deve lançar exceção quando repositório falha")]
        public async Task spRelatorioCategoriaAsync_DeveLancarExcecao_QuandoRepositorioFalha()
        {
            // Arrange
            var dataInicio = DateTime.Today.AddMonths(-1);
            var dataFim = DateTime.Today;

            _relatorioRepositoryMock
                .Setup(r => r.ObterRelatorioByCategoriaAsync(dataInicio, dataFim))
                .ThrowsAsync(new Exception("Erro no banco"));

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() =>
                _service.spRelatorioCategoriaAsync(dataInicio, dataFim));

            Assert.Equal("Erro no banco", ex.Message);
        }
    }
}
