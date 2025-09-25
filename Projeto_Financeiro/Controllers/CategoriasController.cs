using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services.Interfaces;

namespace Projeto_Financeiro.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService _service;

        public CategoriasController(ICategoriasService service)
        {
            _service = service;
        }

        /// <summary>
        /// Lista todas as categorias com paginação e busca.
        /// </summary>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAllCategoria(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            if (page < 1) throw new ArgumentException("O valor de 'page' deve ser maior ou igual a 1.");
            if (pageSize < 1) throw new ArgumentException("O valor de 'pageSize' deve ser maior ou igual a 1.");

            var todasCategorias = (await _service.GetAllCategoriasAsync()).ToList();

            if (!string.IsNullOrWhiteSpace(search))
                todasCategorias = todasCategorias
                    .Where(f => f.Nome.ToLower().Contains(search.ToLower()))
                    .ToList();

            var total = todasCategorias.Count;
            var pagedCategorias = todasCategorias
                .OrderBy(c => c.Nome)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            var response = new
            {
                currentPage = page,
                pageSize,
                total,
                totalPages = (int)Math.Ceiling(total / (double)pageSize),
                items = pagedCategorias
            };

            return Ok(response);
        }

        /// <summary>
        /// Busca uma categoria pelo ID.
        /// </summary>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCategoriasById(int id)
        {
            var categoria = await _service.GetCategoriasByIdAsync(id);
            if (categoria == null)
                throw new KeyNotFoundException($"Categoria com ID {id} não encontrada.");

            return Ok(categoria);
        }

        /// <summary>
        /// Cria uma nova categoria.
        /// </summary>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriasDTO categorias)
        {
            if (categorias == null)
                throw new ArgumentNullException(nameof(categorias), "Dados da categoria não podem ser nulos.");

            var createdCategoria = await _service.CreateCategoriasAsync(categorias);

            if (createdCategoria == null)
                throw new InvalidOperationException("Falha ao criar a categoria.");

            return CreatedAtAction(nameof(GetCategoriasById),
                                   new { id = createdCategoria.Id },
                                   createdCategoria);
        }
    }
}
