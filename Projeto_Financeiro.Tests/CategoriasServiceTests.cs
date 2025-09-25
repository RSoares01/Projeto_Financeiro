using Moq;
using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services;
using Projeto_Financeiro.Domain.Entities;
using Projeto_Financeiro.Domain.Interfaces.IRepositories;

namespace Projeto_Financeiro.Tests
{
    public class CategoriasServiceTests
    {
        private readonly Mock<ICategoriasRepository> _categoriasRepositoryMock;
        private readonly CategoriaService _categoriaService;

        public CategoriasServiceTests()
        {
            _categoriasRepositoryMock = new Mock<ICategoriasRepository>();
            _categoriaService = new CategoriaService(_categoriasRepositoryMock.Object);
        }

        [Fact(DisplayName = "Obter categoria contábil por ID existente")]
        public async Task GetCategoriasByIdAsync_DeveRetornarCategoria_QuandoExistente()
        {
            // Arrange
            var categoria = new Categorias("Folha de Pagamento", 'D', true, DateTime.Now)
            {
                Id = 10
            };

            _categoriasRepositoryMock.Setup(r => r.GetByIdAsync(10)).ReturnsAsync(categoria);

            // Act
            var result = await _categoriaService.GetCategoriasByIdAsync(10);

            // Assert
            Assert.NotNull(result);
            Assert.Equal('D', result.Tipo);
            Assert.Equal("Folha de Pagamento", result.Nome);
        }

        [Fact(DisplayName = "Obter categoria contábil por ID inexistente retorna nulo")]
        public async Task GetCategoriasByIdAsync_DeveRetornarNull_QuandoInexistente()
        {
            // Arrange
            _categoriasRepositoryMock.Setup(r => r.GetByIdAsync(99)).ReturnsAsync((Categorias?)null);

            // Act
            var result = await _categoriaService.GetCategoriasByIdAsync(99);

            // Assert
            Assert.Null(result);
        }

        [Fact(DisplayName = "Listar todas as categorias contábeis")]
        public async Task GetAllCategoriasAsync_DeveRetornarTodasCategorias()
        {
            // Arrange
            var categorias = new List<Categorias>
            {
                new Categorias("Faturamento", 'R', true, DateTime.Now) { Id = 1 },
                new Categorias("Impostos", 'D', true, DateTime.Now) { Id = 2 }
            };

            _categoriasRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(categorias);

            // Act
            var result = await _categoriaService.GetAllCategoriasAsync();

            // Assert
            Assert.NotNull(result);
            Assert.Collection(result,
                item => Assert.Equal('R', item.Tipo),
                item => Assert.Equal('D', item.Tipo));
        }

        [Fact(DisplayName = "Criar nova categoria contábil do tipo Receita")]
        public async Task CreateCategoriasAsync_DeveCriarCategoria_Receita()
        {
            // Arrange
            var dto = new CategoriasDTO
            {
                Nome = "Venda de Produtos",
                Tipo = 'R',
                Ativo = true,
                DataCriacao = DateTime.Now
            };

            // Act
            var result = await _categoriaService.CreateCategoriasAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal('R', result.Tipo);
            Assert.Equal("Venda de Produtos", result.Nome);
        }

        [Fact(DisplayName = "Atualizar categoria contábil existente")]
        public async Task UpdateCategoriasAsync_DeveAtualizarCategoria_QuandoExistente()
        {
            // Arrange
            var existente = new Categorias("Manutenção", 'D', true, DateTime.Now)
            {
                Id = 5
            };

            var dto = new CategoriasDTO
            {
                Id = 5,
                Nome = "Manutenção de Equipamentos",
                Tipo = 'D',
                Ativo = false,
                DataCriacao = existente.DataCriacao
            };

            _categoriasRepositoryMock.Setup(r => r.GetByIdAsync(5)).ReturnsAsync(existente);

            // Act
            var result = await _categoriaService.UpdateCategoriasAsync(dto);

            // Assert
            Assert.NotNull(result);
            Assert.Equal("Manutenção de Equipamentos", result.Nome);
            Assert.False(result.Ativo);
        }

        [Fact(DisplayName = "Atualizar categoria inexistente deve lançar exceção")]
        public async Task UpdateCategoriasAsync_DeveLancarExcecao_QuandoNaoEncontrada()
        {
            // Arrange
            var dto = new CategoriasDTO
            {
                Id = 999,
                Nome = "Categoria Inexistente",
                Tipo = 'D',
                Ativo = true,
                DataCriacao = DateTime.Now
            };

            _categoriasRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Categorias?)null);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => _categoriaService.UpdateCategoriasAsync(dto));
        }

        [Fact(DisplayName = "Remover categoria contábil por ID")]
        public async Task RemoveCategoriasAsync_DeveChamarDelete()
        {
            // Arrange
            var id = 3;
            _categoriasRepositoryMock.Setup(r => r.DeleteAsync(id)).Returns(Task.CompletedTask);

            // Act
            await _categoriaService.RemoveCategoriasAsync(id);

            // Assert
            _categoriasRepositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
        }
    }
}
