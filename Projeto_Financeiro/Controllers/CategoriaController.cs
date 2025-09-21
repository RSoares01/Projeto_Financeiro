using Microsoft.AspNetCore.Mvc;
using Projeto_Financeiro.Application.Services.Interfaces;

namespace Projeto_Financeiro.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CategoriaController : ControllerBase
    {
        private readonly ICategoriasService _categoriasService;

        public CategoriaController(ICategoriasService categoriasService)
        {
            _categoriasService = categoriasService;
        }

        [HttpGet("listar")]
        public async Task<IActionResult> GetAllCategoriaAsync(
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10,
            [FromQuery] string? search = null)
        {
            try
            {
                if (page < 1) page = 1;
                if (pageSize < 10) pageSize = 10;

                var todasCategorias = (await _categoriasService.GetAllCategoriasAsync()).ToList();

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

                return Ok(response); // ✅ melhor que Json()
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro interno: {ex.Message}");
            }
        }
    }
}
