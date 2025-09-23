using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.DTOs;
using Projeto_Financeiro.Application.Services.Interfaces;

namespace Projeto_Financeiro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasService _service;

        public CategoriasController(ICategoriasService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllCategoria(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 10) pageSize = 10;

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
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoriasById(int id)
        {
            try
            {
                var categoria = _service.GetCategoriasByIdAsync(id);
                if (categoria == null)
                    return NotFound(new { message = $"Categoria com ID {id} não encontrado." });

                return Ok(categoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocorreu um erro interno. Tente novamente mais tarde.", details = ex.Message });
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateCategoria([FromBody] CategoriasDTO categorias)
        {
            if (categorias == null)
                return BadRequest(new { error = "Dados de categorias não podem ser nulos." });

            try
            {
                var createdCategoria = await _service.CreateCategoriasAsync(categorias);
                if (createdCategoria == null)
                    return StatusCode(500, new { error = "Falha ao criar a categoria." });

                return CreatedAtAction(nameof(GetCategoriasById),
                                       new { id = createdCategoria.Id },
                                       createdCategoria);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { error = "Ocorreu um erro ao criar a categoria.", details = ex.Message });
            }
        }

    }
}
