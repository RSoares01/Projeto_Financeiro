using Moq;
using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;

namespace Projeto_Financeiro.Tests
{
    public class TransacoesServiceTests
    {
        private readonly Mock<ITransacoesRepository> _transacoesRepositoryMock;
        private readonly TransacoesService _transacoesService;

        public TransacoesServiceTests()
        {
            _transacoesRepositoryMock = new Mock<ITransacoesRepository>();
            _transacoesService = new TransacoesService(_transacoesRepositoryMock.Object);
        }

        [Fact(DisplayName = "Obter transação por ID existente")]
        public async Task GetTransacoesByIdAsync_DeveRetornarTransacao_QuandoExistente()
        {
            // Arrange
            var transacao = new Transacoes(1, "Pagamento de fornecedor", 2500.00m, DateTime.Today, "Referente à compra de materiais", DateTime.Now, 5);
            _transacoesRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(transacao);

            // Act
            var result = await _transacoesService.GetTransacoesByIdAsync(1);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Pagamento de fornecedor", result.Descricao);
            Assert.Equal(2500.00m, result.Valor);
            Assert.Equal(5, result.CategoriaId);
        }

        [Fact(DisplayName = "Obter transação por ID inexistente retorna null")]
        public async Task GetTransacoesByIdAsync_DeveRetornarNull_QuandoInexistente()
        {
            // Arrange
            _transacoesRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Transacoes?)null);

            // Act
            var result = await _transacoesService.GetTransacoesByIdAsync(999);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Listar todas as transações financeiras")]
        public async Task GetAllTransacoesAsync_DeveRetornarTodasTransacoes()
        {
            // Arrange
            var lista = new List<Transacoes>
            {
                new Transacoes(1, "Recebimento de cliente", 5000.00m, DateTime.Today, "Pagamento da fatura", DateTime.Now, 1),
                new Transacoes(2, "Pagamento de imposto", 1200.00m, DateTime.Today.AddDays(-1), "ICMS agosto", DateTime.Now, 2)
            };

            _transacoesRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(lista);

            // Act
            var result = await _transacoesService.GetAllTransacoesAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count());
        }

        [Fact(DisplayName = "Criar nova transação financeira")]
        public async Task CreateTransacoesAsync_DeveCriarTransacao()
        {
            // Arrange
            var dto = new TransacoesDTO
            {
                Id = 0,
                Descricao = "Venda de produto",
                Valor = 3200.50m,
                Data = DateTime.Today,
                Observacoes = "Venda à vista",
                DataCriacao = DateTime.Now,
                CategoriaId = 3
            };

            // Act
            var result = await _transacoesService.CreateTransacoesAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Venda de produto", result.Descricao);
            Assert.Equal(3200.50m, result.Valor);
        }

        [Fact(DisplayName = "Atualizar transação existente")]
        public async Task UpdateTransacoesAsync_DeveAtualizarTransacao_QuandoExistente()
        {
            // Arrange
            var existente = new Transacoes(10, "Compra de equipamentos", 8000.00m, DateTime.Today, "Equipamentos de TI", DateTime.Now, 4);

            var dto = new TransacoesDTO
            {
                Id = 10,
                Descricao = "Compra de equipamentos de informática",
                Valor = 8500.00m,
                Data = DateTime.Today,
                Observacoes = "TI e infraestrutura",
                DataCriacao = existente.DataCriacao,
                CategoriaId = 4
            };

            _transacoesRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(existente);

            // Act
            var result = await _transacoesService.UpdateTransacoesAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Compra de equipamentos de informática", result.Descricao);
            Assert.Equal(8500.00m, result.Valor);
        }

        [Fact(DisplayName = "Atualizar transação inexistente deve lançar exceção")]
        public async Task UpdateTransacoesAsync_DeveLancarExcecao_QuandoNaoEncontrada()
        {
            // Arrange
            var dto = new TransacoesDTO
            {
                Id = 999,
                Descricao = "Lançamento não existente",
                Valor = 100.00m,
                Data = DateTime.Today,
                Observacoes = "",
                DataCriacao = DateTime.Now,
                CategoriaId = 1
            };

            _transacoesRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Transacoes?)null);

            // Act & Assert
            var ex = await Assert.ThrowsAsync<Exception>(() => _transacoesService.UpdateTransacoesAsync(dto));
            Assert.Equal("Transacoes não encontrada.", ex.Message);
        }

        [Fact(DisplayName = "Remover transação por ID")]
        public async Task RemoveTransacoesAsync_DeveChamarDelete()
        {
            // Arrange
            var id = 7;
            _transacoesRepositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _transacoesService.RemoveTransacoesAsync(id);

            // Assert
            _transacoesRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}
